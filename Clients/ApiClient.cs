using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageQaAutomation.Clients;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
        };
    }

    public async Task<HttpResponseMessage> GetPostByIdAsync(int postId)
    {
        return await _httpClient.GetAsync($"/posts/{postId}");
    }

    public async Task<HttpResponseMessage> CreatePostAsync<T>(T requestBody)
    {
        return await _httpClient.PostAsJsonAsync("/posts", requestBody);
    }
}