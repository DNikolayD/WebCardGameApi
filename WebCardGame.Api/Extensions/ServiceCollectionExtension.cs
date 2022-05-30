using FluentValidation;
using WebCardGame.Service.InjectionTypes;

namespace WebCardGame.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var serviceSingletonInterfaceType = typeof(ISingletonService);
            var serviceScopedInterfaceType = typeof(IScopedService);
            var types = serviceInterfaceType.Assembly.GetExportedTypes().Where(x => x.IsClass && !x.IsAbstract).Select(x => new
            {
                Service = x.GetInterface($"I{x.Name}"),
                Implementation = x
            }).Where(x => x.Service != null);

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

            return services;
        }

        public static IServiceCollection AddDataEntityValidators(this IServiceCollection services)
        {
            var validatorType = typeof(AbstractValidator<>);
            var types = validatorType.Assembly.GetExportedTypes().Where(x => x.IsClass && !x.IsAbstract);
            foreach (var type in types)
            {
                services.AddTransient(validatorType, type);
            }
            return services;
        }
    }
}
