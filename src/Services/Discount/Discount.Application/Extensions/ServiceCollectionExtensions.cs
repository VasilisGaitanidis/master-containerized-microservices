using System.Reflection;
using AutoMapper;
using Discount.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscountApplication(this IServiceCollection services)
        {
            services
                .AddCqrsHandlers()
                .AddPipelineBehaviors()
                .AddValidators()
                .AddMappings();

            return services;
        }

        public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}