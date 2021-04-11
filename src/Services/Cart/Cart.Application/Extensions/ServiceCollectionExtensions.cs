﻿using System.Reflection;
using AutoMapper;
using Cart.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCartApplication(this IServiceCollection services)
        {
            return services
                    .AddCqrsHandlers()
                    .AddPipelineBehaviors()
                    .AddValidators()
                    .AddMappings();
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