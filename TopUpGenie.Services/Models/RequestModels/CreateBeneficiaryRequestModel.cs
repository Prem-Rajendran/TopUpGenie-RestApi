namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// CreateBeneficiaryRequestModel
/// </summary>
[ExcludeFromCodeCoverage]
public class CreateBeneficiaryRequestModel
{
    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? BeneficiaryNickname { get; set; }

    [Required]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "UAE Phone Number should be of 7 characters strictly.")]
    public string? BeneficiaryPhoneNumber { get; set; }
}