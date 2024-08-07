namespace TopUpGenie.DataAccess.Repository;

/// <summary>
/// BenificiaryRepository
/// </summary>
public class BenificiaryRepository : Repository<Beneficiary>, IBeneficiaryRepository
{
    private readonly TopUpGenieDbContext _context;
    private readonly ILogger<Repository<Beneficiary>> _logger;


    public BenificiaryRepository(TopUpGenieDbContext context, ILogger<Repository<Beneficiary>> logger) : base(context, logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// GetCountOfMyActiveBeneficiary
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> GetCountOfMyActiveBeneficiary(int userId)
    {
        IEnumerable<Beneficiary> beneficiaries = await _context.Beneficiaries.ToListAsync();
        return beneficiaries.Where(b => (b.CreatedByUserId == userId && b.IsActive == true)).Count();
    }

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    public new async Task<IEnumerable<Beneficiary>> GetAllAsync()
    {

        return await _context.Beneficiaries
            .Include(b => b.BeneficiaryUser)
            .Include(b => b.CreatedByUser)
            .ToListAsync();
    }

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public new async Task<Beneficiary?> GetByIdAsync(int id)
    {
        return await _context.Beneficiaries
            .Include(b => b.BeneficiaryUser)
            .Include(b => b.CreatedByUser)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}