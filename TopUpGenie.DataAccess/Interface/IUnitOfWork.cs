namespace TopUpGenie.DataAccess.Interface;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ISessionRepository Sessions { get; }
    IBeneficiaryRepository Beneficiaries { get; }
    ITopUpOptionsRepository TopUpOptions { get; }

    Task<bool> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}