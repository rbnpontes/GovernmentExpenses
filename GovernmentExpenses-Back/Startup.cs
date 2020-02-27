using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.IO;
using System.Reflection;
using GovernmentExpenses.Core;
using System.Text.RegularExpressions;

namespace GovernmentExpenses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly ILogger logger_;
        public IConfiguration Configuration { get; }
        // This method will load all assemblies in Business folder
        private void ConfigurePlugins(IServiceCollection services)
        {
            try
            {
                // Retrieve only "GovernmentExpenses.*.dll"
                // "GovernmentExpenses.Core.dll" is ignored
                var regex = new Regex("(GovernmentExpenses).*(?<!Core)\\.dll");
                var assembliesPath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Business").Where(x => regex.IsMatch(x));
                var moduleType = typeof(IModule);
                var mvcBuilder = services.AddMvc();
                var mutexObj = new object();
                // Load parallel assemblies
                Parallel.ForEach(assembliesPath, (path) =>
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(path);
                        assembly.GetTypes().Where((type) => type.IsAssignableFrom(moduleType)).ToList().ForEach(type =>
                        {
                        // Instantiate Module
                        IModule module = (IModule)Activator.CreateInstance(type);
                        // Setup this module
                        module.Configure(services);
                        });
                        lock (mutexObj)
                        {
                            // This method will register all controllers on external assembly.
                            mvcBuilder.AddApplicationPart(assembly);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error has Ocurred at Register \"{Path.GetFileName(path)}\"");
                    }
                });
                mvcBuilder.AddControllersAsServices();
            }
            catch (Exception e)
            {
                // [NOTE]: ASP .NET Core 3.1 not work Logger Service at Configure or Startup Constructor
                // Log exception error
                Console.WriteLine("Error has Ocurred at Configure Plugins");
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurePlugins(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable Https if exist Preprocessor, this can be enable at build time.
#if HTTPS
            app.UseHttpsRedirection();
#endif
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
