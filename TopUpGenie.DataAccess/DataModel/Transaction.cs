namespace TopUpGenie.DataAccess.DataModel;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("SourceAccount")]
    public int SourceAccountId { get; set; }

    [ForeignKey("DestinationAccount")]
    public int DestinationAccountId { get; set; }

    [ForeignKey("Beneficiary")]
    public int BeneficiaryId { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public int TransactionFee { get; set; }

    [Required]
    public int TotalAmount { get; set; }

    [Required]
    public string? Currency { get; set; } = "AED";

    [Required]
    public int BalanceBefore { get; set; }

    [Required]
    public int BalanceAfter { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    public User? User { get; set; }

    public Account? SourceAccount { get; set; }

    public Account? DestinationAccount { get; set; }

    public Beneficiary? Beneficiary { get; set; }
}

