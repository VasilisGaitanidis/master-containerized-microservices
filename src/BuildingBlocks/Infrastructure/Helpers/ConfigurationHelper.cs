﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration(string basePath = null)
        {
            basePath ??= Directory.GetCurrentDirectory();
            var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentVariable}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}