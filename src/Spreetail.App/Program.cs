using Microsoft.Extensions.DependencyInjection;
using Spreetail.Infrastructure;

namespace Spreetail.App
{
    class Program
    {
        /// <summary>
        /// Setup app for DI and start app
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Start app
            serviceProvider.GetService<RunProgram<string, string>>().Run();
        }

        /// <summary>
        /// Add services for DI
        /// </summary>
        /// <returns></returns>
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServices<string, string>();
            services.AddTransient<RunProgram<string,string>>();
            return services;
        }
    }
}
