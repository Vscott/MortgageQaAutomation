using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteApiQAAutomation.Clients;
using WebsiteApiQAAutomation.Configuration;
using WebsiteApiQAAutomation.Logging;

namespace WebsiteApiQAAutomation.Base;

/// <summary>
/// Base class for all API test classes.
/// Provides shared setup and reusable framework objects.
/// </summary>
public abstract class BaseTest
{
    /// <summary>
    /// Shared API client available to all test classes.
    /// </summary>
    protected readonly ApiClient ApiClient;

    protected BaseTest()
    {
        TestLogger.LogInfo($"Active test environment: {ConfigurationManager.GetEnvironmentName()}");

        ApiClient = new ApiClient();
    }
}