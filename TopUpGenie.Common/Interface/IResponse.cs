namespace TopUpGenie.Common.Interface;

/// <summary>
/// interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IResponse<T>
{
	T? Data { get; set; }
	Status Status { get; set; }
	List<Message>? Messages { get; set; }
}