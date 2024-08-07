namespace TopUpGenie.Common;

/// <summary>
/// GenericServiceResponse
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericServiceResponse<T> : IResponse<T>
{
    public T? Data { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; }

    public List<Message>? Messages { get; set; }
}