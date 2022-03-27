using Microsoft.Extensions.DependencyInjection;
using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.AllMembersCommandService;
using Spreetail.Core.Services.ClearCommandService;
using Spreetail.Core.Services.ConsoleService;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.KeyExistsCommandService;
using Spreetail.Core.Services.KeysCommandService;
using Spreetail.Core.Services.MemberExistsCommandService;
using Spreetail.Core.Services.MembersCommandService;
using Spreetail.Core.Services.RemoveAllCommandService;
using Spreetail.Core.Services.RemoveCommandService;
using Spreetail.Infrastructure.Domain.ConsoleIO;
using Spreetail.Infrastructure.Services.AddCommandService;
using Spreetail.Infrastructure.Services.AllMembersCommandService;
using Spreetail.Infrastructure.Services.ClearCommandService;
using Spreetail.Infrastructure.Services.DictionaryService;
using Spreetail.Infrastructure.Services.HelpCommandService;
using Spreetail.Infrastructure.Services.KeyCommandService;
using Spreetail.Infrastructure.Services.KeyExistsCommandService;
using Spreetail.Infrastructure.Services.MemberExistsCommandService;
using Spreetail.Infrastructure.Services.MembersCommandService;
using Spreetail.Infrastructure.Services.RemoveAllCommandService;
using Spreetail.Infrastructure.Services.RemoveCommandService;

namespace Spreetail.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices<T,U>(this IServiceCollection services)
        {
            services.AddSingleton<IDictionaryService<T, U>, DictionaryService<T, U>>();
            services.AddScoped<IAddCommandService<T, U>, AddCommandService<T, U>>();
            services.AddScoped<IHelpCommandService, HelpCommandService>();
            services.AddScoped<IKeyCommandService<T, U>, KeyCommandService<T, U>>();
            services.AddScoped<IMembersCommandService<T, U>, MembersCommandService<T, U>>();
            services.AddScoped<IConsoleService, ConsoleService>();
            services.AddScoped<IRemoveCommandService<T,U>, RemoveCommandService<T, U>>();
            services.AddScoped<IRemoveAllCommandService<T,U>, RemoveAllCommandService<T,U>>();
            services.AddScoped<IClearCommandService<T, U>, ClearCommandService<T, U>>();
            services.AddScoped<IKeyExistsCommandService<T, U>, KeyExistsCommandService<T, U>>();
            services.AddScoped<IMemberExistsCommandService<T, U>, MemberExistsCommandService<T, U>>();
            services.AddScoped<IAllMembersCommandService<T, U>, AllMembersCommandService<T, U>>();
            return services;
        }
    }
}
