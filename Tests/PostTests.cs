using FluentAssertions;
using WebsiteApiQAAutomation.Assertions;
using WebsiteApiQAAutomation.Base;
using WebsiteApiQAAutomation.Models;
using WebsiteApiQAAutomation.TestData;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace WebsiteApiQAAutomation.Tests;

/// <summary>
/// Contains automated API tests for post endpoints.
/// </summary>
public class PostTests : BaseTest
{
    /// <summary>
    /// Validates that a single valid post ID returns HTTP 200
    /// and a valid response body.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Valid_Post_Body()
    {
        // Send GET request for post ID 1
        var response = await ApiClient.GetPostByIdAsync(1);

        // Validate status code, response body, and expected post ID
        await ValidateSuccessfulPostResponse(response, 1);
    }

    /// <summary>
    /// Validates multiple valid post IDs using data-driven testing.
    /// </summary>
    [Theory]

    // Each InlineData value runs this test once with a different post ID
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Get_Post_By_Valid_Ids_Should_Return_Ok(int postId)
    {
        // Send GET request using the current post ID
        var response = await ApiClient.GetPostByIdAsync(postId);

        // Validate status code, response body, and expected post ID
        await ValidateSuccessfulPostResponse(response, postId);
    }

    /// <summary>
    /// Validates that an invalid post ID returns HTTP 404 Not Found.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
    {
        // Send GET request using an invalid post ID
        var response = await ApiClient.GetPostByIdAsync(999999);

        // Verify API returns HTTP 404 Not Found
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Validates that a successful GET request returns JSON content.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Json_Content()
    {
        // Send GET request for post ID 1
        var response = await ApiClient.GetPostByIdAsync(1);

        // Verify API returns HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Verify content type header exists
        response.Content.Headers.ContentType.Should().NotBeNull();

        // Verify response media type is JSON
        response.Content.Headers.ContentType!
            .MediaType.Should().Be("application/json");
    }

    /// <summary>
    /// Validates that creating a valid post returns HTTP 201
    /// and response data matches the request data.
    /// </summary>
    [Fact]
    public async Task Create_Post_With_Valid_Data_Should_Return_Created()
    {
        // Create reusable valid request data
        var request = PostRequestBuilder.CreateValidPostRequest();

        // Send POST request with valid request body
        var response = await ApiClient.CreatePostAsync(request);

        // Verify API returns HTTP 201 Created
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Deserialize JSON response into PostResponse model
        var createdPost = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        createdPost.Should().NotBeNull();

        // Verify returned data matches request data
        createdPost!.Title.Should().Be(request.Title);
        createdPost.Body.Should().Be(request.Body);
        createdPost.UserId.Should().Be(request.UserId);

        // Verify API generated an ID
        createdPost.Id.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Validates that creating a post with an empty title
    /// does not cause a server error.
    /// </summary>
    [Fact]
    public async Task Create_Post_With_Empty_Title_Should_Handle_Invalid_Request()
    {
        // Create reusable invalid request data
        var request = PostRequestBuilder.CreatePostRequestWithEmptyTitle();

        // Send POST request with invalid request body
        var response = await ApiClient.CreatePostAsync(request);

        // Verify response exists
        response.Should().NotBeNull();

        // Verify API does not return HTTP 500 Internal Server Error
        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Shared helper method for validating successful post responses.
    /// Keeps repeated response validation in one place.
    /// </summary>
    /// <param name="response">HTTP response returned from the API.</param>
    /// <param name="expectedPostId">Expected post ID returned in the response body.</param>
    private static async Task ValidateSuccessfulPostResponse(
        HttpResponseMessage response,
        int expectedPostId)
    {
        // Verify API returns HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Deserialize JSON response into PostResponse model
        var post = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        post.Should().NotBeNull();

        // Verify returned ID matches expected ID
        post!.Id.Should().Be(expectedPostId);

        // Run reusable post response validation checks
        PostAssertions.ShouldBeValidPost(post);
    }
}