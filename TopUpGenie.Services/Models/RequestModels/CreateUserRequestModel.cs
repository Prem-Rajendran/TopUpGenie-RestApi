namespace TopUpGenie.Services.Models.RequestModels;

/// <summary>
/// CreateUserRequestModel
/// </summary>
[ExcludeFromCodeCoverage]
public class CreateUserRequestModel
{
    [Required]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string? Name { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? Password { get; set; }

    [Required]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "UAE Phone Number should be of 7 characters strictly.")]
    public string? PhoneNumber { get; set; }

    [Required]
    public int InitialBalance { get; set; }

    [Required]
    public bool IsVerified { get; set; } = false;
}