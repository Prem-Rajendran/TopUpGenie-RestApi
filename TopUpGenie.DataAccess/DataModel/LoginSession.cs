namespace TopUpGenie.DataAccess.DataModel;

public class LoginSession
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    public string? AccessToken { get; set; }

    [Required]
    public string? RefreshToken { get; set; }

    public DateTime ExpirationDateTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
