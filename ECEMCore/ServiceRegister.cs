

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECEMCore
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddAppllicationServices(this IServiceCollection services)
        {
          AddServicesToContainer(services); 
          
            return services;
        }
        private static IServiceCollection AddServicesToContainer(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(applicationAssembly);
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(applicationAssembly);
                //Config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                //Config.AddOpenBehavior(typeof(CachingBehaviour<,>));
            });
            return services;
        }
    }
}
