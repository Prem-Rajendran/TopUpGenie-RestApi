namespace TopUpGenie.Services.Interface;

public interface IExternalService
{
    Task<int> GetUserBalanceAsync(int userId);
    Task<bool> DebitUserAccountAsync(int userId, int amount);
    Task<bool> CreditUserAccountAsync(int userId, int amount);
}