namespace TopUpGenie.Services;

/// <summary>
/// TopUpService
/// </summary>
public class TopUpService : ITopUpService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;

    public TopUpService(IUnitOfWork unitOfWork, ITransactionService transactionService)
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
    }

    /// <summary>
    /// ListTopUpOptions
    /// </summary>
    /// <returns></returns>
    public async Task<IResponse<IEnumerable<TopUpOption>>> ListTopUpOptions()
    {
        IResponse<IEnumerable<TopUpOption>> response = new GenericServiceResponse<IEnumerable<TopUpOption>> { Status = Common.Enums.Status.Unknown };

        try
        {
            var topUpOptions = await _unitOfWork.TopUpOptions.GetAllAsync();
            if (topUpOptions != null && topUpOptions.Any())
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = topUpOptions;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.TOPUP_LIST_OPTIONS_FAILED, ErrorMessage.TOPUP_LIST_OPTIONS_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.TOPUP_LIST_OPTIONS_EXCEPTION, ErrorMessage.TOPUP_LIST_OPTIONS_EXCEPTION, ex);
        }

        return response;
    }

    /// <summary>
    /// TopUpTransaction
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<IResponse<bool>> TopUpTransaction(RequestContext context, InitiateTransactionRequestModel requestModel)
    {
        IResponse<bool> response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

        try
        {
            var userTask = _unitOfWork.Users.GetByIdAsync(context.UserId);
            var beneficiaryTask = _unitOfWork.Beneficiaries.GetByIdAsync(requestModel.BeneficiaryId);
            var topUpOptionsTask = _unitOfWork.TopUpOptions.GetByIdAsync(requestModel.TopUpOptionId);

            await Task.WhenAll(userTask, beneficiaryTask, topUpOptionsTask);

            User? user = userTask.Result;
            Beneficiary? beneficiary = beneficiaryTask.Result;
            TopUpOption? topUpOption = topUpOptionsTask.Result;

            if (user != null &&
                beneficiary != null &&
                topUpOption != null &&
                await _transactionService.BeginTransact(response, user, beneficiary, topUpOption))
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.AddMessage(ErrorCodes.TOPUP_TRANSACTION_FAILED, ErrorMessage.TOPUP_TRANSACTION_FAILED);
            }
        }
        catch (Exception ex)
        {
            response.AddMessage(ErrorCodes.TOPUP_TRANSACTION_EXCEPTION, ErrorMessage.TOPUP_TRANSACTION_EXCEPTION, ex);
        }

        return response;
    }
}