using FluentAssertions;
using WebsiteApiQAAutomation.Assertions;
using WebsiteApiQAAutomation.Clients;
using WebsiteApiQAAutomation.Models;
using WebsiteApiQAAutomation.TestData;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using WebsiteApiQAAutomation.Base;

namespace WebsiteApiQAAutomation.Tests;

/// <summary>
/// Contains automated API tests for post endpoints.Vernon was here! :D
/// </summary>

    // Shared API client used by all tests
    public class PostTests : BaseTest
    {

        /// <summary>
        /// Validates that retrieving a valid post ID
        /// returns a valid response body.
        /// </summary>
        [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Valid_Post_Body()
    {
        // Send GET request
        var response = await ApiClient.GetPostByIdAsync(1);

        // Verify HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Deserialize JSON response body
        var post = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        post.Should().NotBeNull();

        // Run reusable validation checks
        PostAssertions.ShouldBeValidPost(post!);

        // Verify specific post ID
        post!.Id.Should().Be(1);
    }

    /// <summary>
    /// Validates that an invalid post ID
    /// returns HTTP 404 Not Found.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
    {
        // Send GET request using invalid ID
        var response = await ApiClient.GetPostByIdAsync(999999);

        // Verify HTTP 404 response
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Validates that a successful GET request
    /// returns JSON content type.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Json_Content()
    {
        // Send GET request
        var response = await ApiClient.GetPostByIdAsync(1);

        // Verify HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Verify content type exists
        response.Content.Headers.ContentType.Should().NotBeNull();

        // Verify API returned JSON
        response.Content.Headers.ContentType!
            .MediaType.Should().Be("application/json");
    }

    /// <summary>
    /// Validates that creating a valid post
    /// returns HTTP 201 Created and correct response data.
    /// </summary>
    [Fact]
    public async Task Create_Post_With_Valid_Data_Should_Return_Created()
    {
        // Build reusable test request data
        var request = PostRequestBuilder.CreateValidPostRequest();

        // Send POST request
        var response = await ApiClient.CreatePostAsync(request);

        // Verify HTTP 201 Created
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Deserialize response body
        var createdPost = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        createdPost.Should().NotBeNull();

        // Verify returned data matches request data
        createdPost!.Title.Should().Be(request.Title);
        createdPost.Body.Should().Be(request.Body);
        createdPost.UserId.Should().Be(request.UserId);

        // Verify generated ID exists
        createdPost.Id.Should().BeGreaterThan(0);
    }
    [Fact] // This test validates that creating a post with an empty title
    public async Task Create_Post_With_Empty_Title_Should_Handle_Invalid_Request()
    {
        // Build invalid request data
        var request = PostRequestBuilder.CreatePostRequestWithEmptyTitle();

        // Send POST request
        var response = await ApiClient.CreatePostAsync(request);

        // Validate response exists
        response.Should().NotBeNull();

        // Validate API responded
        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
    }
    /// <summary>
    /// Validates that multiple valid post IDs
    /// return successful responses and valid response bodies.
    /// 
    /// [Theory] allows the same test to run multiple times
    /// using different input values.
    /// </summary>

    [Theory]

    // InlineData provides different post IDs to test
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Get_Post_By_Valid_Ids_Should_Return_Ok(int postId)
    {
        // Send GET request using current post ID
        var response = await ApiClient.GetPostByIdAsync(postId);

        // Verify API returned HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Deserialize JSON response into PostResponse model
        var post = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        post.Should().NotBeNull();

        // Verify returned post ID matches requested ID
        post!.Id.Should().Be(postId);

        // Run reusable validation checks
        PostAssertions.ShouldBeValidPost(post);
    }
}