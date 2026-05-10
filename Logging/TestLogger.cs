using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteApiQAAutomation.Logging;

/// <summary>
/// Simple framework logger used for test execution visibility.
/// </summary>
public static class TestLogger
{
    /// <summary>
    /// Writes informational log messages to console output.
    /// </summary>
    public static void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
    }

    /// <summary>
    /// Writes error log messages to console output.
    /// </summary>
    public static void LogError(string message)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
    }
}