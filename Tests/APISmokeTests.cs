using FluentAssertions;
using MortgageQaAutomation.Clients;
using MortgageQaAutomation.Models;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MortgageQaAutomation.Tests;

public class ApiSmokeTests
{
    private readonly ApiClient _apiClient = new();

    [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Ok()
    {
        var response = await _apiClient.GetPostByIdAsync(1);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var post = await response.Content.ReadFromJsonAsync<PostResponse>();

        post.Should().NotBeNull();
        post!.Id.Should().Be(1);
        post.UserId.Should().BeGreaterThan(0);
        post.Title.Should().NotBeNullOrWhiteSpace();
        post.Body.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
    {
        var response = await _apiClient.GetPostByIdAsync(999999);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_Post_With_Valid_Data_Should_Return_Created()
    {
        var request = new PostRequest
        {
            Title = "Mortgage QA Automation",
            Body = "Testing API post creation",
            UserId = 1
        };

        var response = await _apiClient.CreatePostAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdPost = await response.Content.ReadFromJsonAsync<PostResponse>();

        createdPost.Should().NotBeNull();
        createdPost!.Title.Should().Be(request.Title);
        createdPost.Body.Should().Be(request.Body);
        createdPost.UserId.Should().Be(request.UserId);
        createdPost.Id.Should().BeGreaterThan(0);
    }
} 