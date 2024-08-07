using Microsoft.EntityFrameworkCore;

namespace TopUpGenie.DataAccess.Interface;

public interface ITransactionRepository
{
    Task<bool> AddAsync(Transaction entity);

    bool Update(Transaction entity);

    Task<int> GetTotalMonthlySpends(int userId);

    Task<int> GetTotalMonthlySpendsPerBeneficiary(int userId, int beneficiaryId);

    Task<IEnumerable<Transaction>?> GetUsersMonthlyTransactions(int userId);

    Task<IEnumerable<Transaction>?> GetLastFiveTransactions();
}