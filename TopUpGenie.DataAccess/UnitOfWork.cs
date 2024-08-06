using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TopUpGenie.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TopUpGenieDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public IUserRepository Users { get; private set; }

        public ISessionRepository Sessions { get; private set; }

        public ITransactionRepository Transactions { get; private set; }

        public IBeneficiaryRepository Beneficiaries { get; private set; }

        public UnitOfWork(TopUpGenieDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
            Users = new UserRepository(_context, loggerFactory.CreateLogger<Repository<User>>());
            Sessions = new SessionRepository(_context, loggerFactory.CreateLogger<Repository<LoginSession>>());
            Transactions = new TransactionRepository(_context, loggerFactory.CreateLogger<Repository<Transaction>>());
            Beneficiaries = new BenificiaryRepository(_context, loggerFactory.CreateLogger<Repository<Beneficiary>>());
        }

        //public async Task<bool> CreateUserWithAccountAsync(User user, Account account)
        //{
        //    bool status = false;
        //    using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            // Create User
        //            await Users.AddAsync(user);
        //            await _context.SaveChangesAsync();

        //            // Create Account
        //            account.UserId = user.Id;
        //            await _context.SaveChangesAsync();

        //            // Commit the transaction
        //            await transaction.CommitAsync();
        //            status = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            _logger.LogError(ex, "Error creating user and account.");
        //        }

        //    }

        //    return status;
        //}

        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save all changes - UnitOfWork", ex);
            }

            return false;
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

