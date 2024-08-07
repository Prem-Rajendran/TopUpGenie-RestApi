namespace TopUpGenie.DataAccess.DataModel;

/// <summary>
/// LoginSession
/// </summary>
public class LoginSession
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    [Required]
    public int UserId { get; set; }

    [Required]
    public string? AccessToken { get; set; }

    [Required]
    public string? RefreshToken { get; set; }

    [Required]
    public DateTime ExpirationDateTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
