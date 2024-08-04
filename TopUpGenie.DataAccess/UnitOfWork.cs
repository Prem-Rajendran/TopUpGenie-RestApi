using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TopUpGenie.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TopUpGenieDbContext _context;
        private readonly ILogger<UnitOfWork> logger;

        public IUserRepository Users { get; private set; }

        public IAccountRepository Accounts { get; private set; }

        public ISessionRepository Sessions { get; private set; }

        public ITransactionRepository Transactions { get; private set; }

        public IBeneficiaryRepository Beneficiaries { get; private set; }

        public UnitOfWork(TopUpGenieDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            logger = loggerFactory.CreateLogger<UnitOfWork>();
            Users = new UserRepository(_context, loggerFactory.CreateLogger<Repository<User>>());
            Accounts = new AccountRepository(_context, loggerFactory.CreateLogger<Repository<Account>>());
            Sessions = new SessionRepository(_context, loggerFactory.CreateLogger<Repository<LoginSession>>());
            Transactions = new TransactionRepository(_context, loggerFactory.CreateLogger<Repository<Transaction>>());
            Beneficiaries = new BenificiaryRepository(_context, loggerFactory.CreateLogger<Repository<Beneficiary>>());
        }

        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError("Failed to save all changes - UnitOfWork", ex);
            }

            return false;
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

