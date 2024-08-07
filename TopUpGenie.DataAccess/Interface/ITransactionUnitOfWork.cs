namespace TopUpGenie.DataAccess.Interface;

/// <summary>
/// ITransactionUnitOfWork
/// </summary>
public interface ITransactionUnitOfWork
{
    /// <summary>
    /// Transactions
    /// </summary>
    ITransactionRepository Transactions { get; }

    /// <summary>
    /// CompleteAsync
    /// </summary>
    /// <returns></returns>
    Task<bool> CompleteAsync();
}