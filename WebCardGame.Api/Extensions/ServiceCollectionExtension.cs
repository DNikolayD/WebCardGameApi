using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebCardGame.Common.Configuration;
using WebCardGame.Data;
using WebCardGame.Data.DataEntities.IdentityDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Service.InjectionTypes;

namespace WebCardGame.Api.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void PipeLine(this IServiceCollection services, ConfigurationManager configuration, AuthOptions authOptions)
        {
            services.AddSingleton(authOptions);
            services.AddControllers();
            services.AddCustomIdentity();
            services.AddCustomDb(configuration);
            //services.AddDataEntityValidators();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddServices();
            services.AddScoped<ApplicationInitializer>();
            services.AddEndpointsApiExplorer();
            services.AddCustomSwagger();
            services.AddCustomAuthentication(authOptions);
        }

        private static void AddServices(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var serviceSingletonInterfaceType = typeof(ISingletonService);
            var serviceScopedInterfaceType = typeof(IScopedService);
            var types = serviceInterfaceType.Assembly
                .GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new
                {
                    Service = x.GetInterface($"I{x.Name}"),
                    Implementation = x
                })
                .Where(x => x.Service != null);

            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (serviceSingletonInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (serviceScopedInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }
        }

        /*        private static void AddDataEntityValidators(this IServiceCollection services)
                {
                    var validatorType = typeof(AbstractValidator<>);
                    var types = validatorType.Assembly.GetExportedTypes().Where(x => x.IsClass && !x.IsAbstract);
                    foreach (var type in types)
                    {
                        services.AddTransient(validatorType, type);
                    }
                }*/

        private static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserDataEntity, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }

        private static void AddCustomDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).LogTo(message => File.AppendAllText(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\logs.txt"), message)));
        }

        private static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCardGame.Api", Version = "v1" });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Please enter JWT with Bearer into field"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static void AddCustomAuthentication(this IServiceCollection services, AuthOptions authOptions)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(authOptions.SecurityKeyAsBytes),
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
