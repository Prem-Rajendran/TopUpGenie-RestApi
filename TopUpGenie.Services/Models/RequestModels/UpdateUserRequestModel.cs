﻿namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// UpdateUserRequestModel
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserRequestModel
{
	[Required]
	public int UserId { get; set; }

    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string? Name { get; set; }

    [StringLength(7, MinimumLength = 7, ErrorMessage = "Phone Number should be of 7 characters strictly.")]
    public string? PhoneNumber { get; set; }

    public int Money { get; set; }

    public bool? IsVerified { get; set; }

    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? NewPassword { get; set; }

    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? ConfirmPassword { get; set; }

    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? OldPassword { get; set; }
}