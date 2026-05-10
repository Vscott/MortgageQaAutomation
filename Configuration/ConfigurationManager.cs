using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebsiteApiQAAutomation.Configuration;

/// <summary>
/// Handles framework configuration loading.
/// Supports environment-specific appsettings files.
/// </summary>
public static class ConfigurationManager
{
    // Reads the environment name from TEST_ENVIRONMENT.
    // Defaults to "qa" if no environment is provided.
    private static readonly string EnvironmentName =
        Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "qa";

    // Loads base appsettings.json first, then overrides with environment-specific settings.
    private static readonly IConfigurationRoot Configuration =
        new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
            .Build();

    /// <summary>
    /// Returns API base URL from configuration.
    /// </summary>
    public static string GetBaseUrl()
    {
        return Configuration["ApiSettings:BaseUrl"]!;
    }

    /// <summary>
    /// Returns the active test environment.
    /// </summary>
    public static string GetEnvironmentName()
    {
        return EnvironmentName;
    }
}