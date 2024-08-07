using System;
namespace TopUpGenie.Services.Models.RequestModels
{
	public class InitiateTransactionRequestModel
	{
		public int BeneficiaryId { get; set; }
		public int TopUpOptionId { get; set; }
    }
}