namespace TopUpGenie.Services.Interface;

/// <summary>
/// ITopUpService
/// </summary>
public interface ITopUpService
{
    /// <summary>
    /// ListTopUpOptions
    /// </summary>
    /// <returns></returns>
    Task<IResponse<IEnumerable<TopUpOption>>> ListTopUpOptions();

    /// <summary>
    /// TopUpTransaction
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    Task<IResponse<bool>> TopUpTransaction(RequestContext context, InitiateTransactionRequestModel requestModel);
}