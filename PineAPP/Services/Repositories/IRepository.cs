using System.Linq.Expressions;

namespace PineAPP.Services.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);

    void Update(T entity);

    int SaveChanges();

    Task<int> SaveChangesAsync();
}