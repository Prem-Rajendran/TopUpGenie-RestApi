namespace TopUpGenie.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionUnitOfWork _transactionUnitOfWork;

    public ProfileService(IUnitOfWork unitOfWork, ITransactionUnitOfWork transactionUnitOfWork)
		{
        _unitOfWork = unitOfWork;
        _transactionUnitOfWork = transactionUnitOfWork;
    }

    public async Task<IResponse<ProfileResponseModel>> GetMyProfile(RequestContext requestContext)
    {
        IResponse<ProfileResponseModel> response = new GenericServiceResponse<ProfileResponseModel> { Status = Common.Enums.Status.Unknown };
        try
        {
            var userTask = _unitOfWork.Users.GetByIdAsync(requestContext.UserId);
            var beneficiaryTask = _unitOfWork.Beneficiaries.GetAllAsync();
            var transactionTask = _transactionUnitOfWork.Transactions.GetUsersMonthlyTransactions(requestContext.UserId);

            await Task.WhenAll(userTask, transactionTask, beneficiaryTask);

            User? user = userTask.Result;
            IEnumerable<Transaction>? transactions = transactionTask.Result;
            IEnumerable<Beneficiary>? beneficiaries = beneficiaryTask.Result;

            if (user != null)
            {
                ProfileResponseModel profile = new() { UserDetails = new UserDto(user) };
                if (transactions != null && transactions.Any())
                {
                    profile.LastFiveTransactions = transactions.TakeLast(5).Select(s => new TransactionDto(s));
                    profile.TotalMonthlyTransaction = transactions.Sum(t => t.TransactionAmount);
                }

                if (beneficiaries != null && beneficiaries.Any())
                {
                    profile.Beneficiaries = beneficiaries
                        .Where(b => b.CreatedByUserId == requestContext.UserId)
                        .Select(b => new BeneficiaryTransactionDto(transactions, b));
                }

                response.Data = profile;
                response.Status = Common.Enums.Status.Success;
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

