using System.Text.Json.Serialization;

namespace TopUpGenie.Common;

public class GenericServiceResponse<T> : IResponse<T>
{
    public T Data { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; }

    public List<Message> Messages { get; set; }
}