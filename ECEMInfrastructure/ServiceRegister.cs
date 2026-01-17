
using ECEMCore.Abstraction.Caching;
using ECEMCore.Abstraction.Emailing;
using ECEMInfrastructure.Repositories;
using ECEMInfrastructure.Services.Caching;
using ECEMInfrastructure.Services.Emailling;
using ECEMInfrastructure.UnitOfWorks;
using ECMDomain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ECEMInfrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructureDependencies(
            this IServiceCollection services,IConfiguration config)
        {
            AddDbConnection(services,config);
            AddServicesToContainer(services);
            AddCaching(services, config);
            AddHealthChecks(services, config);
            return services;
        }

        private static IServiceCollection AddDbConnection(
          this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseSqlServer(config.GetConnectionString("DataBase"));
            });

            return services;

        }

        private static IServiceCollection AddServicesToContainer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepostiry<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitWork, UnitOfWork>(); 
            //** After Docker
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }


        private static IServiceCollection AddCaching(
        this IServiceCollection services,
         IConfiguration config)
        {
            services.AddStackExchangeRedisCache(opt
                => opt.Configuration = config.GetConnectionString("RedisConnection"));

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
        private static IServiceCollection AddHealthChecks(
         this IServiceCollection services,
         IConfiguration config)
        {
            services.AddHealthChecks()
                .AddSqlServer(config.GetConnectionString("Database")!)
                .AddRedis(config.GetConnectionString("RedisConnection")!);

            return services;
        }
    }
}
