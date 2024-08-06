namespace TopUpGenie.Services;

public class MyExternalService : IExternalService
{
    private readonly IUnitOfWork _unitOfWork;

	public MyExternalService(IUnitOfWork unitOfWork)
	{
        _unitOfWork = unitOfWork;
	}

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

    public async Task<int> GetUserBalanceAsync(int userId)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user != null)
            return user.Balance;

        return 0;
    }
}

