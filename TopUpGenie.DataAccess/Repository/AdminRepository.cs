using System;
namespace TopUpGenie.DataAccess.Repository
{
	public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly TopUpGenieDbContext _context;
        private readonly ILogger<Repository<Admin>> _logger;


        public AdminRepository(TopUpGenieDbContext context, ILogger<Repository<Admin>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}