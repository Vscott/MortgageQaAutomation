using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebsiteApiQAAutomation.Configuration;

/// <summary>
/// Handles framework configuration loading.
/// </summary>
public static class ConfigurationManager
{
    // Load configuration from appsettings.json
    private static readonly IConfigurationRoot Configuration =
        new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

    /// <summary>
    /// Returns API base URL from configuration.
    /// </summary>
    public static string GetBaseUrl()
    {
        return Configuration["ApiSettings:BaseUrl"]!;
    }
}