namespace TopUpGenie.Services;

/// <summary>
/// TransactionService
/// </summary>
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

    /// <summary>
    /// BeginTransact
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <param name="beneficiary"></param>
    /// <param name="topUpOption"></param>
    /// <returns></returns>
    public async Task<bool> BeginTransact(IResponse<bool> response, User user, Beneficiary beneficiary, TopUpOption topUpOption)
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
            TransactionStatus = DataAccess.Enums.TransactionStatus.INITIATED,
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
                transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.FAILED;
                transaction.Messages += $"{ErrorCodes.TRANSACTION_MONTHLY_LIMIT_REACHED} - {ErrorMessage.TRANSACTION_MONTHLY_LIMIT_REACHED};";
                response.AddMessage(ErrorCodes.TRANSACTION_MONTHLY_LIMIT_REACHED, ErrorMessage.TRANSACTION_MONTHLY_LIMIT_REACHED);
                _transactionUnitOfWork.Transactions.Update(transaction);
                await _transactionUnitOfWork.CompleteAsync();
                throw new Exception(ErrorCodes.TRANSACTION_MONTHLY_LIMIT_REACHED);
            }

            // Begin Transaction
            await _unitOfWork.BeginTransactionAsync();

            // Check Balance
            int balance = await _externalService.GetUserBalanceAsync(user.Id);

            // Check if balance is sufficient
            if (balance < totalAmount)
            {
                transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.FAILED;
                transaction.Messages += $"{ErrorCodes.TRANSACTION_INSUFICIENT_BALANCE} - {ErrorMessage.TRANSACTION_INSUFICIENT_BALANCE};";
                response.AddMessage(ErrorCodes.TRANSACTION_INSUFICIENT_BALANCE, ErrorMessage.TRANSACTION_INSUFICIENT_BALANCE);
                _transactionUnitOfWork.Transactions.Update(transaction);
                await _transactionUnitOfWork.CompleteAsync();
                throw new Exception(ErrorCodes.TRANSACTION_INSUFICIENT_BALANCE);
            }

            // Debit User Account
            bool debitSuccess = await _externalService.DebitUserAccountAsync(user.Id, totalAmount);
            if (!debitSuccess)
            {
                transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.FAILED;
                transaction.Messages += $"{ErrorCodes.TRANSACTION_DEBIT_FAILED} - {ErrorMessage.TRANSACTION_DEBIT_FAILED};";
                response.AddMessage(ErrorCodes.TRANSACTION_DEBIT_FAILED, ErrorMessage.TRANSACTION_DEBIT_FAILED);
                _transactionUnitOfWork.Transactions.Update(transaction);
                throw new Exception(ErrorCodes.TRANSACTION_DEBIT_FAILED);
            }

            bool creditSuccess = await _externalService.CreditUserAccountAsync(beneficiary.BeneficiaryUser.Id, topUpOption.Amount);
            if (!creditSuccess)
            {
                transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.FAILED;
                transaction.Messages += $"{ErrorCodes.TRANSACTION_CREDIT_FAILED} - {ErrorMessage.TRANSACTION_CREDIT_FAILED};";
                response.AddMessage(ErrorCodes.TRANSACTION_CREDIT_FAILED, ErrorMessage.TRANSACTION_CREDIT_FAILED);
                _transactionUnitOfWork.Transactions.Update(transaction);
                throw new Exception(ErrorCodes.TRANSACTION_CREDIT_FAILED);
            }

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.SUCCESS;
            _transactionUnitOfWork.Transactions.Update(transaction);
            await _transactionUnitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            transaction.TransactionStatus = DataAccess.Enums.TransactionStatus.FAILED;
            _transactionUnitOfWork.Transactions.Update(transaction);
            await _transactionUnitOfWork.CompleteAsync();

            // Trigger a Orchestrated Mechanism or function like LAMBDA or a Batch job to identify failed transaction and initiate refund process if necessary.

            switch (ex.Message)
            {
                case ErrorCodes.TRANSACTION_CREDIT_FAILED:
                    // Refund the money to debited user
                    // or
                    // Retry transaction again
                break;
            }
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