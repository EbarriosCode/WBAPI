using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WBAPI.Application.Behaviors;

namespace WBAPI.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            var assembly = typeof(DependencyContainer).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>),
                                  typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
