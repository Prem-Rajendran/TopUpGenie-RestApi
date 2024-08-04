namespace TopUpGenie.DataAccess.Interface;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}