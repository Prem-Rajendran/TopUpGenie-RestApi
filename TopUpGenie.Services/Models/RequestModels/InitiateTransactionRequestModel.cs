namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// InitiateTransactionRequestModel
/// </summary>
public class InitiateTransactionRequestModel
{
	public int BeneficiaryId { get; set; }
	public int TopUpOptionId { get; set; }
}