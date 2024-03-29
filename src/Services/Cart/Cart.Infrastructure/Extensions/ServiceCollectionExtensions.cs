﻿using System;
using Cart.Domain.Repositories;
using Cart.Infrastructure.Repositories;
using Cart.Infrastructure.Services.Grpc;
using Discount.Grpc.Protos;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Infrastructure.Extensions
{
    /// <summary>
    /// The cart infrastructure <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add cart infrastructure on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCartInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomRedis(configuration)
                .AddConsulServiceDiscovery(configuration)
                .AddGrpcServices(configuration);
        }

        /// <summary>
        /// Add cart repositories on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICartRepository, CartRepository>();
        }

        /// <summary>
        /// Add cart redis cache on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCustomRedis(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetConnectionString("Redis"));
        }

        /// <summary>
        /// Add gRPC services on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient. more info can be found here: 
            // https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-5.0#call-insecure-grpc-services-with-net-core-client
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services
                .AddScoped<IDiscountGrpcService, DiscountGrpcService>()
                .AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(x =>
                    x.Address = new Uri(configuration.GetConnectionString("DiscountGrpcUrl")));

            return services;
        }
    }
}