namespace TopUpGenie.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionUnitOfWork _transactionUnitOfWork;
    private readonly IExternalService _externalService;

    public TransactionService(IUnitOfWork unitOfWork, ITransactionUnitOfWork transactionUnitOfWork, IExternalService externalService)
    {
        _unitOfWork = unitOfWork;
        _externalService = externalService;
        _transactionUnitOfWork = transactionUnitOfWork;
    }

    public async Task<bool> BeginTransact(User user, Beneficiary beneficiary, TopUpOption topUpOption)
    {
        // Calculate total amount
        int totalAmount = 1 + topUpOption.Amount;

        Transaction transaction = new()
        {
            TransactionDate = DateTime.Now,
            UserId = user.Id,
            BeneficiaryId = beneficiary.Id,
            TopUpOptionId = topUpOption.TopUpOptionId,
            TransactionFee = 1,
            TransactionStatus = "Initiated",
            Messages = "",
            TransactionAmount = topUpOption.Amount,
            TotalTransactionAmount = totalAmount
        };

        try
        {
            await _transactionUnitOfWork.Transactions.AddAsync(transaction);
            await _transactionUnitOfWork.CompleteAsync();

            if (!await IsEligibleForTransaction(user, beneficiary, totalAmount))
            {
                transaction.TransactionStatus = "Failure";
                transaction.Messages += "Monthly Transaction Limit Reached;";
                _transactionUnitOfWork.Transactions.Update(transaction);
                await _transactionUnitOfWork.CompleteAsync();
                throw new Exception("Monthly Transaction Limit Reached");
            }

            // Begin Transaction
            await _unitOfWork.BeginTransactionAsync();

            // Check Balance
            int balance = await _externalService.GetUserBalanceAsync(user.Id);

            // Check if balance is sufficient
            if (balance < totalAmount)
            {
                transaction.TransactionStatus = "Failure";
                transaction.Messages += "Insufficient Balance;";
                _transactionUnitOfWork.Transactions.Update(transaction);
                await _transactionUnitOfWork.CompleteAsync();
                throw new Exception("Insufficient Balance");
            }

            // Debit User Account
            bool debitSuccess = await _externalService.DebitUserAccountAsync(user.Id, totalAmount);
            if (!debitSuccess)
            {
                transaction.Messages += "Failed to debt user account;";
                _transactionUnitOfWork.Transactions.Update(transaction);
                throw new Exception("Failed to debt user account");
            }

            bool creditSuccess = await _externalService.CreditUserAccountAsync(beneficiary.BeneficiaryUser.Id, topUpOption.Amount);
            if (!creditSuccess)
            {
                transaction.Messages += "Failed to credit user account;";
                _transactionUnitOfWork.Transactions.Update(transaction);
                throw new Exception("Failed to credit user account");
            }

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            transaction.TransactionStatus = "Success";
            _transactionUnitOfWork.Transactions.Update(transaction);
            await _transactionUnitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            transaction.TransactionStatus = "Failure";
            _transactionUnitOfWork.Transactions.Update(transaction);
            await _transactionUnitOfWork.CompleteAsync();

            // Trigger a Orchestrated Mechanism or function like LAMBDA or a Batch job to identify failed transaction and initiate refund process if necessary.
        }

        return false;
    }

    private async Task<bool> IsEligibleForTransaction(User user, Beneficiary beneficiary, int totalAmount)
    {
        int totalMonthlySpends = await _transactionUnitOfWork.Transactions.GetTotalMonthlySpends(user.Id);

        int totalMonthlySpendsPerBeneficiary = await _transactionUnitOfWork.Transactions.GetTotalMonthlySpendsPerBeneficiary(user.Id, beneficiary.Id);

        if (user.Verified)
            return (totalMonthlySpends <= 3000 - totalAmount) && (totalMonthlySpendsPerBeneficiary <= 500 - totalAmount);

        return (totalMonthlySpends <= 3000 - totalAmount) && (totalMonthlySpendsPerBeneficiary <= 1000 - totalAmount);
    }
}