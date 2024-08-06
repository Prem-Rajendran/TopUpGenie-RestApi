namespace TopUpGenie.DataAccess.Interface;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ISessionRepository Sessions { get; }
    ITransactionRepository Transactions { get; }
    IBeneficiaryRepository Beneficiaries { get; }

    Task<bool> CompleteAsync();
}