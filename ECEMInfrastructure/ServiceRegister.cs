
using Asp.Versioning;
using ECEMCore.Abstraction.Caching;
using ECEMCore.Abstraction.Emailing;
using ECEMCore.Abstraction.TokenProviding;
using ECEMInfrastructure.Outbox;
using ECEMInfrastructure.Repositories;
using ECEMInfrastructure.Services.Caching;
using ECEMInfrastructure.Services.Emailling;
using ECEMInfrastructure.Services.TokenProviding;
using ECEMInfrastructure.UnitOfWorks;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Roles;
using ECMDomain.Entities.Identity.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;
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
            AddApiVersioning(services);
            AddBackgroundJobs(services, config);
            AddIdentity(services, config);
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
        private static IServiceCollection AddApiVersioning(
         this IServiceCollection services)
        {
            services
                .AddApiVersioning(opt =>
                {
                    opt.DefaultApiVersion = new ApiVersion(1);
                    opt.ReportApiVersions = true;
                    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddMvc()
                .AddApiExplorer(opt =>
                {
                    opt.GroupNameFormat = "'v'V";
                    opt.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        private static IServiceCollection AddBackgroundJobs(
        this IServiceCollection services,
        IConfiguration config)
        {
            services.Configure<OutboxOptions>(config.GetSection("Outbox"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobsSetup>();

            return services;
        }

        private static IServiceCollection AddIdentity(
       this IServiceCollection services,
       IConfiguration config)
        {
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredUniqueChars = 2;
                    opt.Password.RequiredLength = 8;
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JWT:Secret"]!)),
                        ValidateLifetime = false,
                        ValidIssuer = config["JWT:Issuer"],
                        ValidAudience = config["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<TokenSettings>(config.GetSection("JWT"));

            services.AddTransient<ITokenService, TokenService>();

            return services;
        }



    }
}
