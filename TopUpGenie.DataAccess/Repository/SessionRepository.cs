namespace TopUpGenie.DataAccess.Repository;

/// <summary>
/// SessionRepository
/// </summary>
public class SessionRepository : Repository<LoginSession>, ISessionRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<LoginSession>> _logger;

    public SessionRepository(TopUpGenieDbContext context, ILogger<Repository<LoginSession>> logger) : base(context, logger)
    {
        _context = context;
        _logger = logger;
    }
}

