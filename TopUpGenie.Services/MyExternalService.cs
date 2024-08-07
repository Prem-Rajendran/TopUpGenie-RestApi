namespace TopUpGenie.Services;

/// <summary>
/// MyExternalService
/// </summary>
public class MyExternalService : IExternalService
{
    private readonly IUnitOfWork _unitOfWork;

	public MyExternalService(IUnitOfWork unitOfWork)
	{
        _unitOfWork = unitOfWork;
	}

    /// <summary>
    /// CreditUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public async Task<bool> CreditUserAccountAsync(int userId, int amount)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user != null)
        {
            user.Balance += amount;
            _unitOfWork.Users.Update(user);
            return true;
        }

        return false;
    }

    /// <summary>
    /// DebitUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public async Task<bool> DebitUserAccountAsync(int userId, int amount)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user != null && user.Balance >= amount)
        {
            user.Balance -= amount;
            _unitOfWork.Users.Update(user);
            return true;
        }

        return false;
    }

    /// <summary>
    /// GetUserBalanceAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> GetUserBalanceAsync(int userId)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user != null)
            return user.Balance;

        return 0;
    }
}

