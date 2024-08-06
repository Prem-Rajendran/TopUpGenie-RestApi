using System;
namespace TopUpGenie.Services.Models.RequestModels
{
	public class UpdateBeneficiaryRequestModel
	{
        [Required]
        public int BeneficiaryId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string? BeneficiaryNickname { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number should be of 10 characters strictly.")]
        public string? BeneficiaryPhoneNumber { get; set; }
    }
}

