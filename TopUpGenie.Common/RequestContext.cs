namespace TopUpGenie.Common;

	public class RequestContext
	{
    public string? TrackingId { get; set; }
    public string? SessionId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }

    public RequestContext(HttpContext httpContext)
    {
        // Extract data from HttpContext and headers
        TrackingId = httpContext.Request.Headers["X-Tracking-ID"].ToString();
        SessionId = httpContext.Request.Headers["X-Session-ID"].ToString();
        AccessToken = httpContext.Request.Headers["Authorization"].ToString();
        RefreshToken = httpContext.Request.Headers["X-Refresh-Token"].ToString();

        // For user-related information, assume authentication middleware sets these properties
        var user = httpContext.User;
        UserId = user?.FindFirst("sub")?.Value; // For JWT token claims
        Name = user?.Identity?.Name; // For user name
    }
}