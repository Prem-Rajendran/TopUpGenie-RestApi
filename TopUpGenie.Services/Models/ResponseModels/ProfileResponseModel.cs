namespace TopUpGenie.Services.Models.ResponseModels;

/// <summary>
/// ProfileResponseModel
/// </summary>
public class ProfileResponseModel
{
	public UserDto? UserDetails { get; set; }

	public int TotalMonthlyTransaction { get; set; }

	public IEnumerable<BeneficiaryTransactionDto>? Beneficiaries { get; set; }

    public IEnumerable<TransactionDto>? LastFiveTransactions { get; set; }
}