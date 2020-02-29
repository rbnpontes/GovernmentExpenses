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
                        assembly.DefinedTypes.Where((type) => type.GetInterfaces().Contains(moduleType)).ToList().ForEach(type =>
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
                        Console.WriteLine(e.ToString());
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
#if SWAGGER
            // Swagger Generation: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Government Expenses",
                    Version = "v1",
                    Description = "Simple Proof of Concept!\n" +
                                   "This system uses a Pernambuco Government open dataset - Recife, Brazil<br/>"+
                                   $"Architecture of this system is a Plugin System, this system will lookup a <bloquote>{AppDomain.CurrentDomain.BaseDirectory}\\Business</blockquote><br/>" +
                                   $"and Load all DLL's inside at this folder.<br/>" +
                                   $"Business modules contains a 'IModule' interface implemented, this is used for register all services.",
                    Contact=new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "rbnpontes@gmail.com",
                        Name = "Ruben Gomes",
                        Url = new Uri("http://github.com/rbnpontes")
                    }

                });
                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Docs").ToList().ForEach(x => c.IncludeXmlComments(x));
            });
#endif
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
#if SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GovernmentExpenses API V1");
            });
#endif
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
