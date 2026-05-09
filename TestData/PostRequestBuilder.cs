using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MortgageQaAutomation.Models;

namespace MortgageQaAutomation.TestData;

/// <summary>
/// Provides reusable test data objects.
/// Helps avoid duplicated request setup in tests.
/// </summary>
public static class PostRequestBuilder
{
    /// <summary>
    /// Creates a valid post request object.
    /// </summary>
    /// <returns>Valid PostRequest object</returns>
    public static PostRequest CreateValidPostRequest()
    {
        return new PostRequest
        {
            Title = "Mortgage QA Automation",
            Body = "Testing API post creation",
            UserId = 1
        };
    }

    /// <summary>
    /// Creates a post request with an empty title.
    /// Useful for negative testing scenarios.
    /// </summary>
    /// <returns>Invalid PostRequest object</returns>
    public static PostRequest CreatePostRequestWithEmptyTitle()
    {
        return new PostRequest
        {
            Title = string.Empty,
            Body = "Testing missing title validation",
            UserId = 1
        };
    }
}