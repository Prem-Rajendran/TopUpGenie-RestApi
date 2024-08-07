namespace TopUpGenie.RestApi.Extensions;

/// <summary>
/// ResponseExtension
/// </summary>
public static class ResponseExtension
{
    /// <summary>
    /// ToApiResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static IResponse<T> ToApiResponse<T>(this IResponse<T> response, HttpContext httpContext)
	{
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

		if (response.Status == Common.Enums.Status.Success)
            httpContext.Response.StatusCode = StatusCodes.Status200OK;

		else if (response.Status == Common.Enums.Status.Failure)
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        return response;
	}
}