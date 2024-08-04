namespace TopUpGenie.DataAccess.Interface;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetAccount(string accountNumber);

    Task<int> GetAccountBalance(int accountId);

    Task<int> GetAccountBalance(string accountNumber);
}