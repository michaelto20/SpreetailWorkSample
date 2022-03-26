using Microsoft.Extensions.DependencyInjection;
using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Infrastructure.Services.AddCommandService;
using Spreetail.Infrastructure.Services.DictionaryService;
using Spreetail.Infrastructure.Services.HelpCommandService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IDataService<string, string>, DataService>();
            services.AddSingleton<IDictionaryService<string, string>, DictionaryService<string, string>>();
            services.AddScoped<IAddCommandService<string, string>, AddCommandService<string, string>>();
            services.AddScoped<IHelpCommandService, HelpCommandService>();
            return services;
        }
    }
}
