namespace TopUpGenie.Services.Interface;

/// <summary>
/// ITransactionService
/// </summary>
public interface ITransactionService
{
    /// <summary>
    /// BeginTransact
    /// </summary>
    /// <param name="response"></param>
    /// <param name="user"></param>
    /// <param name="beneficiary"></param>
    /// <param name="topUpOption"></param>
    /// <returns></returns>
    Task<bool> BeginTransact(IResponse<bool> response, User user, Beneficiary beneficiary, TopUpOption topUpOption);
}