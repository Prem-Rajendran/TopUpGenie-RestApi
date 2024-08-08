namespace TopUpGenie.Services.Models.Dto;

/// <summary>
/// TransactionDto
/// </summary>
[ExcludeFromCodeCoverage]
public class TransactionDto
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int BeneficiaryUserId { get; set; }

    public string? BeneficiaryUserName { get; set; }

    public string? BeneficiaryName { get; set; }

    public int? TopUpAmount { get; set; }

    public int TransactionFee { get; set; }

    public int TotalTransactionAmount { get; set; }

    public TransactionStatus? TransactionStatus { get; set; }

    public string? Messages { get; set; }

    public DateTime TransactionDate { get; set; }

    public TransactionDto(Transaction transaction)
    {
        if (transaction != null)
        {
            TransactionId = transaction.Id;
            UserId = transaction.UserId;
            UserName = transaction.User?.Name;
            BeneficiaryUserId = transaction.BeneficiaryId;
            BeneficiaryName = transaction.Beneficiary?.Nickname;
            BeneficiaryUserName = transaction.Beneficiary?.BeneficiaryUser?.Name;
            TopUpAmount = transaction.TopUpOption?.Amount;
            TransactionFee = transaction.TransactionFee;
            TotalTransactionAmount = transaction.TotalTransactionAmount;
            TransactionStatus = transaction.TransactionStatus;
            Messages = transaction.Messages;
            TransactionDate = transaction.TransactionDate;
        }
    }
}