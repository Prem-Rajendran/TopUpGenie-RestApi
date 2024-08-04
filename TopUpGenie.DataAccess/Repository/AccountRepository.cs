namespace TopUpGenie.DataAccess.Repository;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<Account>> _logger;

    public AccountRepository(TopUpGenieDbContext context, ILogger<Repository<Account>> logger) : base(context, logger)
	{
        _logger = logger;
        _context = context;
	}

    public async Task<int> GetAccountBalance(int accountId)
    {
        try
        {
            if (accountId != 0)
            {
                Account? account = await _context.Accounts.FindAsync(accountId);
                return account != null ? account.Balance : 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Get Account Balance By Account Id Failed", ex);
        }

        return 0;
    }

    public async Task<int> GetAccountBalance(string accountNumber)
    {
        try
        {
            Account? account = await GetAccount(accountNumber);
            return account != null ? account.Id : 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Get Account Balance By Account Number Failed", ex);
        }

        return 0;
    }

    public async Task<Account?> GetAccount(string accountNumber)
    {
        try
        {
            if (accountNumber != null)
            {
                return await _context.Accounts.FirstOrDefaultAsync(account => account.AccountNumber == accountNumber);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Get Account By Account Number Failed", ex);
        }

        return null;
    }
}

