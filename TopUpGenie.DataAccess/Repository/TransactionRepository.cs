namespace TopUpGenie.DataAccess.Repository;

/// <summary>
/// TransactionRepository
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionDbContext _context;
    private readonly ILogger<Repository<Transaction>> _logger;

    public TransactionRepository(TransactionDbContext context, ILogger<Repository<Transaction>> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(Transaction entity)
    {
        try
        {
            await _context.Transactions.AddAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Add Async Failed", ex);
        }

        return false;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(Transaction entity)
    {
        try
        {
            _context.Transactions.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Update Failed", ex);
        }

        return false;
    }

    /// <summary>
    /// GetLastFiveTransactions
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>?> GetLastFiveTransactions()
    {
        try
        {
            IEnumerable<Transaction> transactions = await _context.Transactions
                .Include(e => e.Beneficiary)
                .Include(e => e.User)
                .Include(e => e.TopUpOption)
                .ToListAsync();

            return transactions.TakeLast(5);
        }
        catch(Exception ex)
        {
            _logger.LogError("Failed to get transaction list", ex);
        }

        return null;
    }

    /// <summary>
    /// GetTotalMonthlySpends
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> GetTotalMonthlySpends(int userId)
    {
        IEnumerable<Transaction>? transactions = await GetUsersMonthlyTransactions(userId);
        if (transactions != null && transactions.Any())
            return transactions.Sum(t => t.TransactionAmount);
        return 0;
    }

    /// <summary>
    /// GetTotalMonthlySpendsPerBeneficiary
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="beneficiaryId"></param>
    /// <returns></returns>
    public async Task<int> GetTotalMonthlySpendsPerBeneficiary(int userId, int beneficiaryId)
    {
        IEnumerable<Transaction>? transactions = await GetUsersMonthlyTransactions(userId);
        if (transactions != null && transactions.Any())
            return transactions
                .Where(t => t.BeneficiaryId == beneficiaryId)
                .Sum(t => t.TransactionAmount);

        return 0;
    }

    /// <summary>
    /// GetUsersMonthlyTransactions
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>?> GetUsersMonthlyTransactions(int userId)
    {
        try
        {
            IEnumerable<Transaction> transactions = await _context.Transactions.ToListAsync();
            if (transactions != null && transactions.Any())
            {
                return transactions.Where(t => t.UserId == userId &&
                    t.TransactionStatus == Enums.TransactionStatus.SUCCESS &&
                    t.TransactionDate.Month == DateTime.Now.Month &&
                    t.TransactionDate.Year == DateTime.Now.Year);
            }

            return transactions;
        }
        catch(Exception ex)
        {
            _logger.LogError("Failed to get transaction list", ex);
        }

        return null;
    }
}

