using Microsoft.Extensions.DependencyInjection;
using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.ConsoleService;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.KeysCommandService;
using Spreetail.Core.Services.MembersCommandService;
using Spreetail.Core.Services.RemoveCommandService;
using Spreetail.Infrastructure.Domain.ConsoleIO;
using Spreetail.Infrastructure.Services.AddCommandService;
using Spreetail.Infrastructure.Services.DictionaryService;
using Spreetail.Infrastructure.Services.HelpCommandService;
using Spreetail.Infrastructure.Services.KeyCommandService;
using Spreetail.Infrastructure.Services.MembersCommandService;
using Spreetail.Infrastructure.Services.RemoveCommandService;

namespace Spreetail.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices<T,U>(this IServiceCollection services)
        {
            //services.AddScoped<IDataService<string, string>, DataService>();
            services.AddSingleton<IDictionaryService<T, U>, DictionaryService<T, U>>();
            services.AddScoped<IAddCommandService<T, U>, AddCommandService<T, U>>();
            services.AddScoped<IHelpCommandService, HelpCommandService>();
            services.AddScoped<IKeyCommandService<T, U>, KeyCommandService<T, U>>();
            services.AddScoped<IMembersCommandService<T, U>, MembersCommandService<T, U>>();
            services.AddScoped<IConsoleService, ConsoleService>();
            services.AddScoped<IRemoveCommandService<T,U>, RemoveCommandService<T, U>>();
            return services;
        }
    }
}
