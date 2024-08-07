namespace TopUpGenie.Services.Interface;

/// <summary>
/// IExternalService
/// </summary>
public interface IExternalService
{
    /// <summary>
    /// GetUserBalanceAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> GetUserBalanceAsync(int userId);

    /// <summary>
    /// DebitUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task<bool> DebitUserAccountAsync(int userId, int amount);

    /// <summary>
    /// CreditUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task<bool> CreditUserAccountAsync(int userId, int amount);
}