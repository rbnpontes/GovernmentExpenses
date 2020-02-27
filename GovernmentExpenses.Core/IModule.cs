using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Core
{
    public interface IModule
    {
        void Configure(IServiceCollection services);
    }
}
