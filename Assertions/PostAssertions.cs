using FluentAssertions;
using MortgageQaAutomation.Models;

namespace MortgageQaAutomation.Assertions;

public static class PostAssertions
{
    public static void ShouldBeValidPost(PostResponse post)
    {
        post.Id.Should().BeGreaterThan(0);
        post.UserId.Should().BeGreaterThan(0);
        post.Title.Should().NotBeNullOrWhiteSpace();
        post.Body.Should().NotBeNullOrWhiteSpace();
    }
}