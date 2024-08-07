namespace TopUpGenie.DataAccess.Interface;

/// <summary>
/// ITransactionRepository
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddAsync(Transaction entity);

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool Update(Transaction entity);

    /// <summary>
    /// GetTotalMonthlySpends
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> GetTotalMonthlySpends(int userId);

    /// <summary>
    /// GetTotalMonthlySpendsPerBeneficiary
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="beneficiaryId"></param>
    /// <returns></returns>
    Task<int> GetTotalMonthlySpendsPerBeneficiary(int userId, int beneficiaryId);

    /// <summary>
    /// GetUsersMonthlyTransactions
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<Transaction>?> GetUsersMonthlyTransactions(int userId);

    /// <summary>
    /// GetLastFiveTransactions
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Transaction>?> GetLastFiveTransactions();
}