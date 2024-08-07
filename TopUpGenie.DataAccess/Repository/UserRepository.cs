namespace TopUpGenie.DataAccess.Repository;

/// <summary>
/// UserRepository
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<User>> _logger;


    public UserRepository(TopUpGenieDbContext context, ILogger<Repository<User>> logger) : base(context, logger)
	{
        _context = context;
        _logger = logger;
	}

    /// <summary>
    /// GetUserByPhoneNumber
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return null;

        return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
    }
}