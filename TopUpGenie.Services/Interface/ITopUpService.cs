namespace TopUpGenie.Services.Interface;

public interface ITopUpService
{
    Task<IResponse<IEnumerable<TopUpOption>>> ListTopUpOptions();

    Task<IResponse<bool>> TopUpTransaction(RequestContext context, InitiateTransactionRequestModel requestModel);
}