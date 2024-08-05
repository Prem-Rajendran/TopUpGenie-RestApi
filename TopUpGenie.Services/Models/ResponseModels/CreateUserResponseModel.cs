using System;
namespace TopUpGenie.Services.Models.ResponseModels
{
	public class CreateUserResponseModel
	{
		public int UserId { get; set; }
		public string? AccountNumber { get; set; }
    }
}