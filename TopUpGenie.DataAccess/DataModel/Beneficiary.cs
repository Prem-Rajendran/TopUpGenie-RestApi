namespace TopUpGenie.DataAccess.DataModel;

/// <summary>
/// Beneficiary
/// </summary>
[ExcludeFromCodeCoverage]
public class Beneficiary
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? Nickname { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int CreatedByUserId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    public User? BeneficiaryUser { get; set; }

    [ForeignKey("CreatedByUserId")]
    public User? CreatedByUser { get; set; }
}

