namespace TopUpGenie.DataAccess.DataModel;

public class Beneficiary
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? Nickname { get; set; }

    [Required]
    [ForeignKey("BeneficiaryUser")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("CreatedByUser")]
    public int CreatedByUserId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? BeneficiaryUser { get; set; }

    public User? CreatedByUser { get; set; }
}

