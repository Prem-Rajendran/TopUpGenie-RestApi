using Microsoft.EntityFrameworkCore;
using TopUpGenie.DataAccess.Extensions;

namespace TopUpGenie.DataAccess;

/// <summary>
/// UnitOfWork
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Users
    /// </summary>
    public IUserRepository Users { get; private set; }

    /// <summary>
    /// Sessions
    /// </summary>
    public ISessionRepository Sessions { get; private set; }

    /// <summary>
    /// Beneficiaries
    /// </summary>
    public IBeneficiaryRepository Beneficiaries { get; private set; }

    /// <summary>
    /// TopUpOptions
    /// </summary>
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
        catch(DbUpdateException ex)
        {
            _context.HandleDbUpdateException(ex);
            _logger.LogError("Failed to save all changes - UnitOfWork", ex);
        }

        return false;
        
    }

    /// <summary>
    /// BeginTransactionAsync
    /// </summary>
    /// <returns></returns>
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// CommitAsync
    /// </summary>
    /// <returns></returns>
    public async Task CommitAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    /// <summary>
    /// RollbackAsync
    /// </summary>
    /// <returns></returns>
    public async Task RollbackAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }


    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}

