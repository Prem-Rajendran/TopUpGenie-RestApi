namespace TopUpGenie.DataAccess.DataModel;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    [ForeignKey("Beneficiary")]
    public int BeneficiaryId { get; set; }
    public Beneficiary? Beneficiary { get; set; }

    [Required]
    [ForeignKey("TopUpOption")]
    public int TopUpOptionId { get; set; }
    public TopUpOption? TopUpOption { get; set; }

    [Required]
    public int TransactionAmount { get; set; }

    [Required]
    public int TransactionFee { get; set; }

    [Required]
    public string? TransactionStatus { get; set; }

    [Required]
    public string? Messages { get; set; }

    [Required]
    public int TotalTransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }
}

