namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// UpdateBeneficiaryRequestModel
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateBeneficiaryRequestModel
{
    [Required]
    public int BeneficiaryId { get; set; }

    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? BeneficiaryNickname { get; set; }

    [StringLength(7, MinimumLength = 7, ErrorMessage = "UAE Phone Number should be of 7 characters strictly.")]
    public string? BeneficiaryPhoneNumber { get; set; }
}

