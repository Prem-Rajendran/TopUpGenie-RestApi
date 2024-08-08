namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// InitiateTransactionRequestModel
/// </summary>
[ExcludeFromCodeCoverage]
public class InitiateTransactionRequestModel
{
	public int BeneficiaryId { get; set; }
	public int TopUpOptionId { get; set; }
}