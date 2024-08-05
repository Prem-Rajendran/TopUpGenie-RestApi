namespace TopUpGenie.DataAccess.Interface;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IAccountRepository Accounts { get; }
    ISessionRepository Sessions { get; }
    ITransactionRepository Transactions { get; }
    IBeneficiaryRepository Beneficiaries { get; }
    IAdminRepository AdminUsers { get; }

    Task<bool> CreateUserWithAccountAsync(User user, Account account);
    Task<bool> CompleteAsync();
}