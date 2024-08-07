namespace TopUpGenie.Services.Extensions;

/// <summary>
/// MessageExtensions
/// </summary>
public static class MessageExtensions
{
    /// <summary>
    /// AddMessage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="errorCode"></param>
    /// <param name="description"></param>
    public static void AddMessage<T>(this IResponse<T> response, string errorCode, string description)
    {
        response.Messages ??= new List<Message>();
        response.Messages.Add(new Message { ErrorCode = errorCode, Description = description });
    }

    /// <summary>
    /// AddMessage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="errorCode"></param>
    /// <param name="description"></param>
    /// <param name="ex"></param>
    public static void AddMessage<T>(this IResponse<T> response, string errorCode, string description, Exception ex)
    {
        response.Messages ??= new List<Message>();
        response.Messages.Add(new Message { ErrorCode = errorCode, Description = string.Format(description, ex.Message) });
    }
}