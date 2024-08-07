namespace TopUpGenie.Services;

public class TopUpService : ITopUpService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;

    public TopUpService(IUnitOfWork unitOfWork, ITransactionService transactionService)
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
    }

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
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
            {
                new Message
                {
                    ErrorCode = "",
                    Description = ""
                }
            };
        }

        return response;
    }

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
                await _transactionService.BeginTransact(user, beneficiary, topUpOption))
            {
                response.Status = Common.Enums.Status.Success;
                response.Data = true;
            }
            else
            {
                response.Status = Common.Enums.Status.Failure;
                response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
            }
        }
        catch (Exception ex)
        {
            response.Messages = new List<Message>
                {
                    new Message
                    {
                        ErrorCode = "",
                        Description = ""
                    }
                };
        }

        return response;
    }
}

