namespace TopUpGenie.DataAccess.Interface;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    Task<int> GetCountOfMyActiveBeneficiary(int userId);
    new Task<IEnumerable<Beneficiary>> GetAllAsync();
    new Task<Beneficiary?> GetByIdAsync(int id);
}