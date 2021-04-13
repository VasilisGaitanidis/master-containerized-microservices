using System.Reflection;
using AutoMapper;
using Discount.Application.Behaviors;
using FluentValidation;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.Extensions
{
    /// <summary>
    /// The discount application <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecksBuilder = services.AddHealthChecks();

            healthChecksBuilder.AddSqlServer(
                configuration.GetConnectionString("DiscountSqlServer"),
                name: "DiscountSqlServer-check",
                tags: new[] { "discountsqlserver" });

            return services;
        }

    }
}