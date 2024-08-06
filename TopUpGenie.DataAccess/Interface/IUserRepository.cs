namespace TopUpGenie.DataAccess.Interface;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
}

