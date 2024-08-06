namespace TopUpGenie.Services.Models.RequestModels;

public class CreateUserRequestModel
{
    [Required]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string? Name { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 4, ErrorMessage = "Password should be within 4 character to 12 characters strictly")]
    public string? Password { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number should be of 10 characters strictly.")]
    public string? PhoneNumber { get; set; }

    [Required]
    public int InitialBalance { get; set; }

    [Required]
    public bool IsVerified { get; set; } = false;
}