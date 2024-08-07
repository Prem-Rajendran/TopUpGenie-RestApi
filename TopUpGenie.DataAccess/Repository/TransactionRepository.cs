using Microsoft.EntityFrameworkCore;

namespace TopUpGenie.DataAccess.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionDbContext _context;
    private readonly ILogger<Repository<Transaction>> _logger;

    public TransactionRepository(TransactionDbContext context, ILogger<Repository<Transaction>> logger)
    {
        _context = context;
        _logger = logger;
    }

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

    public async Task<IEnumerable<Transaction>> GetLastFiveTransactions()
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

    public async Task<int> GetTotalMonthlySpends(int userId)
    {
        IEnumerable<Transaction>? transactions = await GetUsersMonthlyTransactions(userId);
        if (transactions != null && transactions.Any())
            return transactions.Sum(t => t.TransactionAmount);
        return 0;
    }

    public async Task<int> GetTotalMonthlySpendsPerBeneficiary(int userId, int beneficiaryId)
    {
        IEnumerable<Transaction>? transactions = await GetUsersMonthlyTransactions(userId);
        if (transactions != null && transactions.Any())
            return transactions
                .Where(t => t.BeneficiaryId == beneficiaryId)
                .Sum(t => t.TransactionAmount);

        return 0;
    }

    public async Task<IEnumerable<Transaction>?> GetUsersMonthlyTransactions(int userId)
    {
        try
        {
            IEnumerable<Transaction> transactions = await _context.Transactions.ToListAsync();
            if (transactions != null && transactions.Any())
            {
                return transactions.Where(t => t.UserId == userId &&
                    t.TransactionStatus == "Success" &&
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

