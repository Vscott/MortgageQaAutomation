using FluentAssertions;
using WebsiteApiQAAutomation.Models;

namespace WebsiteApiQAAutomation.Assertions;

/// <summary>
/// Contains reusable assertions for validating post responses.
/// Keeps validation logic centralized and reusable.
/// </summary>
public static class PostAssertions
{
    /// <summary>
    /// Validates that a post response contains valid data.
    /// </summary>
    /// <param name="post">Post response object</param>
    public static void ShouldBeValidPost(PostResponse post)
    {
        // Verify ID is greater than zero
        post.Id.Should().BeGreaterThan(0);

        // Verify UserId is greater than zero
        post.UserId.Should().BeGreaterThan(0);

        // Verify title is populated
        post.Title.Should().NotBeNullOrWhiteSpace();

        // Verify body content is populated
        post.Body.Should().NotBeNullOrWhiteSpace();
    }
}