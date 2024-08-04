using System;
using TopUpGenie.Common.Enums;

namespace TopUpGenie.Common.Interface
{
	public interface IResponse<T>
	{
		T Data { get; set; }
		Status Status { get; set; }
		List<Message> Messages { get; set; }
	}
}