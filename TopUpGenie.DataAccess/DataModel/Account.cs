namespace TopUpGenie.DataAccess.DataModel;

public class Account
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    [Required]
    public int UserId { get; set; }

    [Required]
    public string? AccountNumber { get; set; }

    [Required]
    public int Balance { get; set; }

    [Required]
    public string? Currency { get; set; } = "AED";

    public User? User { get; set; }
}

