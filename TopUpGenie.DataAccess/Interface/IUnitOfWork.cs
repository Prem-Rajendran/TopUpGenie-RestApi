namespace TopUpGenie.DataAccess.Interface;

/// <summary>
/// IUnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Users
    /// </summary>
    IUserRepository Users { get; }

    /// <summary>
    /// Sessions
    /// </summary>
    ISessionRepository Sessions { get; }

    /// <summary>
    /// Beneficiaries
    /// </summary>
    IBeneficiaryRepository Beneficiaries { get; }

    /// <summary>
    /// TopUpOptions
    /// </summary>
    ITopUpOptionsRepository TopUpOptions { get; }

    /// <summary>
    /// CompleteAsync
    /// </summary>
    /// <returns></returns>
    Task<bool> CompleteAsync();

    /// <summary>
    /// BeginTransactionAsync
    /// </summary>
    /// <returns></returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// CommitAsync
    /// </summary>
    /// <returns></returns>
    Task CommitAsync();

    /// <summary>
    /// RollbackAsync
    /// </summary>
    /// <returns></returns>
    Task RollbackAsync();
}