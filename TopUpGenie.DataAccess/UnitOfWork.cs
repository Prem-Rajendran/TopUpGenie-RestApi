namespace TopUpGenie.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private IDbContextTransaction? _transaction;

    public IUserRepository Users { get; private set; }

    public ISessionRepository Sessions { get; private set; }

    public IBeneficiaryRepository Beneficiaries { get; private set; }

    public ITopUpOptionsRepository TopUpOptions { get; private set; }

    public UnitOfWork(TopUpGenieDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger<UnitOfWork>();

        Users = new UserRepository(_context, loggerFactory.CreateLogger<Repository<User>>());
        Sessions = new SessionRepository(_context, loggerFactory.CreateLogger<Repository<LoginSession>>());
        Beneficiaries = new BenificiaryRepository(_context, loggerFactory.CreateLogger<Repository<Beneficiary>>());
        TopUpOptions = new TopUpOptionsRepository(_context, loggerFactory.CreateLogger<Repository<TopUpOption>>());
    }

    public async Task<bool> CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError("Failed to save all changes - UnitOfWork", ex);
        }

        return false;
        
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

