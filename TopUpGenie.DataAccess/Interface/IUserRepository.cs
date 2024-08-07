namespace TopUpGenie.DataAccess.Interface;

/// <summary>
/// IUserRepository
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// GetUserByPhoneNumber
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
}

