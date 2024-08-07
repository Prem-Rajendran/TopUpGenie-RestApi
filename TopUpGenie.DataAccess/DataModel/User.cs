namespace TopUpGenie.DataAccess.DataModel;

/// <summary>
/// User
/// </summary>
[ExcludeFromCodeCoverage]
public class User
{
    [Key]
    public int Id { get; set; }

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
    public bool Verified { get; set; }

    [Required]
    public bool IsAdmin { get; set; }

    [Required]
    public int Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public IEnumerable<LoginSession>? LoginSessions { get; set; }
}