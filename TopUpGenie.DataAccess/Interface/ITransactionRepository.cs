using Microsoft.EntityFrameworkCore;

namespace TopUpGenie.DataAccess.Interface;

public interface ITransactionRepository
{
    Task<bool> AddAsync(Transaction entity);

    bool Update(Transaction entity);

    Task<int> GetTotalMonthlySpends(int userId);

    Task<int> GetTotalMonthlySpendsPerBeneficiary(int userId, int beneficiaryId);
}