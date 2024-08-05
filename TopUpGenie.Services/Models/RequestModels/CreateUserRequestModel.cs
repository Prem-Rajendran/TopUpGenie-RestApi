namespace TopUpGenie.Services.Models.RequestModels;

public class CreateUserRequestModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public int InitialBalance { get; set; }

    [Required]
    public bool IsVerified { get; set; } = false;

    [Required]
    public string Currency { get; set; } = "AED";
}