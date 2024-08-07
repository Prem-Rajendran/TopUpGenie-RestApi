namespace TopUpGenie.DataAccess.Interface;

/// <summary>
/// IBeneficiaryRepository
/// </summary>
public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    /// <summary>
    /// GetCountOfMyActiveBeneficiary
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> GetCountOfMyActiveBeneficiary(int userId);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    new Task<IEnumerable<Beneficiary>> GetAllAsync();

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    new Task<Beneficiary?> GetByIdAsync(int id);
}