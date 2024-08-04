namespace TopUpGenie.DataAccess.DataModel;

public class Beneficiary
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? Nickname { get; set; }

    [Required]
    public int AccountId { get; set; }

    [ForeignKey("User")]
    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User;
}

