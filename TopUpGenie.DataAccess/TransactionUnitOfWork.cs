namespace TopUpGenie.DataAccess;

/// <summary>
/// TransactionUnitOfWork
/// </summary>
public class TransactionUnitOfWork : ITransactionUnitOfWork
{
    private readonly TransactionDbContext _context;
    private readonly ILogger<TransactionUnitOfWork> _logger;

    public ITransactionRepository Transactions { get; private set; }

    /// <summary>
    /// TransactionUnitOfWork
    /// </summary>
    /// <param name="context"></param>
    /// <param name="loggerFactory"></param>
    public TransactionUnitOfWork(TransactionDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger<TransactionUnitOfWork>();
        Transactions = new TransactionRepository(_context, loggerFactory.CreateLogger<Repository<Transaction>>());
    }

    /// <summary>
    /// CompleteAsync
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to save all changes - UnitOfWork", ex);
        }

        return false;
    }
}