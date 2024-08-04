namespace TopUpGenie.DataAccess.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<Transaction>> _logger;

    public TransactionRepository(TopUpGenieDbContext context, ILogger<Repository<Transaction>> logger) : base(context, logger)
    {
        _context = context;
        _logger = logger;
    }
}

