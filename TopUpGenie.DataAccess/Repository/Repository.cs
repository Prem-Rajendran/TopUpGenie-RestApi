namespace TopUpGenie.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TopUpGenieDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ILogger<Repository<T>> _logger;


    public Repository(TopUpGenieDbContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _logger = logger;
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Add Async Failed", ex);
        }

        return false;
    }

    public bool Delete(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Delete Failed", ex);

        }
        return false;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get All Async Failed", ex);
        }

        return new List<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            T? entity = await _dbSet.FindAsync(id);
            return entity ?? null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Get By Id Async Failed", ex);
        }

        return null;
    }

    public bool Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Update Failed", ex);
        }

        return false;
    }
}

