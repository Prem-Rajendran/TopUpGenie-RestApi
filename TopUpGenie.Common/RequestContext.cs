namespace TopUpGenie.Common;

public class RequestContext
{
    public string? TrackingId { get; set; }
    public string? SessionId { get; set; }
    public string? Authorization { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int UserId { get; set; }

    public RequestContext(HttpContext httpContext)
    {
        // Extract data from HttpContext and headers
        TrackingId = httpContext.Request.Headers["X-Tracking-ID"].ToString();
        SessionId = httpContext.Request.Headers["X-Session-ID"].ToString();
        Authorization = httpContext.Request.Headers["Authorization"].ToString();
        RefreshToken = httpContext.Request.Headers["X-Refresh-Token"].ToString();
        AccessToken = httpContext.Request.Headers["X-Access-Token"].ToString();

        _ = int.TryParse(httpContext.Request.Headers["X-User-ID"].ToString(), out int id);
        UserId = id;
    }
}