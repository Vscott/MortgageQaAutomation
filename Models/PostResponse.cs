using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteApiQAAutomation.Models;

/// <summary>
/// Represents the API response body for a post.
/// </summary>
public class PostResponse
{
    // User associated with the post
    public int UserId { get; set; }

    // Unique post ID
    public int Id { get; set; }

    // Title returned from the API
    public string Title { get; set; } = string.Empty;

    // Body content returned from the API
    public string Body { get; set; } = string.Empty;
}