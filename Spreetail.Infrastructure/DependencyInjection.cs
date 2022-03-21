using Microsoft.Extensions.DependencyInjection;
using Spreetail.Core.Services.DataService;
using Spreetail.Infrastructure.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataService, DataService>();
            return services;
        }
    }
}
