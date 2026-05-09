using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageQaAutomation.Models;

/// <summary>
/// Represents the request body used when creating a post.
/// </summary>
public class PostRequest
{
    // Title of the post
    public string Title { get; set; } = string.Empty;

    // Main content/body of the post
    public string Body { get; set; } = string.Empty;

    // User associated with the post
    public int UserId { get; set; }
}