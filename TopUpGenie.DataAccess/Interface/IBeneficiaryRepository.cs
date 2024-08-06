namespace TopUpGenie.DataAccess.Interface;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    Task<int> GetCountOfMyActiveBeneficiary(int userId);
}