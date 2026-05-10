using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace WebsiteApiQAAutomation.Clients;

/// <summary>
/// Handles API communication for the test framework.
/// This class centralizes HTTP requests so tests stay clean.
/// </summary>
public class ApiClient
{
    // Shared HttpClient instance for sending requests
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor initializes the base API URL.
    /// </summary>
    public ApiClient()
    {
        _httpClient = new HttpClient
        {
            // Base URL for all requests
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
        };
    }

    /// <summary>
    /// Sends a GET request to retrieve a post by ID.
    /// </summary>
    /// <param name="postId">Post ID to retrieve</param>
    /// <returns>HTTP response from the API</returns>
    public async Task<HttpResponseMessage> GetPostByIdAsync(int postId)
    {
        return await _httpClient.GetAsync($"/posts/{postId}");
    }

    /// <summary>
    /// Sends a POST request to create a new post.
    /// </summary>
    /// <typeparam name="T">Request body type</typeparam>
    /// <param name="requestBody">Object to serialize into JSON</param>
    /// <returns>HTTP response from the API</returns>
    public async Task<HttpResponseMessage> CreatePostAsync<T>(T requestBody)
    {
        return await _httpClient.PostAsJsonAsync("/posts", requestBody);
    }
}