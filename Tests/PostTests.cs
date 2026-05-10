using FluentAssertions;
using WebsiteApiQAAutomation.Assertions;
using WebsiteApiQAAutomation.Base;
using WebsiteApiQAAutomation.Logging;
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
        // Log test start
        TestLogger.LogInfo("Starting valid post retrieval test.");

        // Send GET request for post ID 1
        var response = await ApiClient.GetPostByIdAsync(1);

        // Log received response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Validate successful response
        await ValidateSuccessfulPostResponse(response, 1);

        // Log successful test completion
        TestLogger.LogInfo("Valid post retrieval test completed successfully.");
    }

    /// <summary>
    /// Validates multiple valid post IDs using data-driven testing.
    /// </summary>
    [Theory]

    // Each InlineData value runs the same test with different post IDs
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Get_Post_By_Valid_Ids_Should_Return_Ok(int postId)
    {
        // Log current test execution
        TestLogger.LogInfo($"Starting data-driven validation for post ID: {postId}");

        // Send GET request using current post ID
        var response = await ApiClient.GetPostByIdAsync(postId);

        // Log response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Validate successful response
        await ValidateSuccessfulPostResponse(response, postId);

        // Log successful validation
        TestLogger.LogInfo($"Validation completed successfully for post ID: {postId}");
    }

    /// <summary>
    /// Validates that an invalid post ID returns HTTP 404 Not Found.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
    {
        // Log test start
        TestLogger.LogInfo("Starting invalid post ID validation test.");

        // Send GET request using invalid post ID
        var response = await ApiClient.GetPostByIdAsync(999999);

        // Log response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Verify API returns HTTP 404 Not Found
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        // Log successful validation
        TestLogger.LogInfo("Invalid post ID validation completed successfully.");
    }

    /// <summary>
    /// Validates that a successful GET request returns JSON content.
    /// </summary>
    [Fact]
    public async Task Get_Post_By_Valid_Id_Should_Return_Json_Content()
    {
        // Log test start
        TestLogger.LogInfo("Starting JSON content validation test.");

        // Send GET request for post ID 1
        var response = await ApiClient.GetPostByIdAsync(1);

        // Log response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Verify API returns HTTP 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Verify content type header exists
        response.Content.Headers.ContentType.Should().NotBeNull();

        // Verify API returned JSON content
        response.Content.Headers.ContentType!
            .MediaType.Should().Be("application/json");

        // Log successful validation
        TestLogger.LogInfo("JSON content validation completed successfully.");
    }

    /// <summary>
    /// Validates that creating a valid post returns HTTP 201
    /// and response data matches request data.
    /// </summary>
    [Fact]
    public async Task Create_Post_With_Valid_Data_Should_Return_Created()
    {
        // Log test start
        TestLogger.LogInfo("Starting valid post creation test.");

        // Create reusable valid request data
        var request = PostRequestBuilder.CreateValidPostRequest();

        // Send POST request using valid request body
        var response = await ApiClient.CreatePostAsync(request);

        // Log response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Verify API returns HTTP 201 Created
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Deserialize JSON response into PostResponse model
        var createdPost = await response.Content.ReadFromJsonAsync<PostResponse>();

        // Verify response body exists
        createdPost.Should().NotBeNull();

        // Verify response data matches request data
        createdPost!.Title.Should().Be(request.Title);
        createdPost.Body.Should().Be(request.Body);
        createdPost.UserId.Should().Be(request.UserId);

        // Verify generated ID exists
        createdPost.Id.Should().BeGreaterThan(0);

        // Log successful test completion
        TestLogger.LogInfo("Valid post creation test completed successfully.");
    }

    /// <summary>
    /// Validates that creating a post with an empty title
    /// does not cause a server error.
    /// </summary>
    [Fact]
    public async Task Create_Post_With_Empty_Title_Should_Handle_Invalid_Request()
    {
        // Log test start
        TestLogger.LogInfo("Starting invalid post creation validation.");

        // Create reusable invalid request data
        var request = PostRequestBuilder.CreatePostRequestWithEmptyTitle();

        // Send POST request using invalid request body
        var response = await ApiClient.CreatePostAsync(request);

        // Log response status code
        TestLogger.LogInfo($"Received response status code: {response.StatusCode}");

        // Verify response exists
        response.Should().NotBeNull();

        // Verify API did not return HTTP 500 Internal Server Error
        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);

        // Log successful validation
        TestLogger.LogInfo("Invalid post validation completed successfully.");
    }

    /// <summary>
    /// Shared helper method for validating successful post responses.
    /// Centralizes reusable response validation logic.
    /// </summary>
    /// <param name="response">HTTP response returned from the API.</param>
    /// <param name="expectedPostId">Expected post ID returned in response body.</param>
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

        // Verify returned post ID matches expected post ID
        post!.Id.Should().Be(expectedPostId);

        // Run reusable validation checks
        PostAssertions.ShouldBeValidPost(post);
    }
}