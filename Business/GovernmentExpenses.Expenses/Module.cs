using GovernmentExpenses.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses
{
    public sealed class Module : IModule
    {
        public void Configure(IServiceCollection services)
        {
            ILoggerFactory factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole().AddDebug();
            });
            services.AddSingleton(factory.CreateLogger("Expenses"));
        }
    }
}
