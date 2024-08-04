namespace TopUpGenie.DataAccess.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<User>> _logger;


    public UserRepository(TopUpGenieDbContext context, ILogger<Repository<User>> logger) : base(context, logger)
	{
        _context = context;
        _logger = logger;
	}
}