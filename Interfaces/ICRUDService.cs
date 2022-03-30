using System.Linq.Expressions;
namespace marketplace.Interfaces;

public interface ICRUDService<T> where T : class
{
    Task<T> Add(T obj);
    Task<T> Update(T obj);
    Task<bool> Delete(int id);
    Task<List<T>> GetAll();
    Task<List<T>> Get(int limit, int offset, string keyword);
    Task<T?> Get(int id);

    Task<T?> Get(Expression<Func<T, bool>> func);
}
