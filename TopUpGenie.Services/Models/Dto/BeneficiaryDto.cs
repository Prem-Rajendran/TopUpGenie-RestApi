
namespace TopUpGenie.Services.Models.Dto;

/// <summary>
/// BeneficiaryDto
/// </summary>
public class BeneficiaryDto
{
	public int BeneficiaryId { get; set; }
	public string? BeneficiaryNickName { get; set; }
	public string? BeneficiaryPhoneNumber { get; set; }
	public bool IsActive { get; set; }

	public BeneficiaryDto(Beneficiary beneficiary)
	{
		BeneficiaryId = beneficiary.Id;
		BeneficiaryNickName = beneficiary.Nickname;
		BeneficiaryPhoneNumber = beneficiary.BeneficiaryUser?.PhoneNumber ?? "";
		IsActive = beneficiary.IsActive;
    }
}