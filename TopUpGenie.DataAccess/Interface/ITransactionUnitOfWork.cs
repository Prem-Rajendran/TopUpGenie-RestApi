namespace TopUpGenie.DataAccess.Interface;

public interface ITransactionUnitOfWork
{
    ITransactionRepository Transactions { get; }
    Task<bool> CompleteAsync();
}