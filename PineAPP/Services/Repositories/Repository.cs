using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;

namespace PineAPP.Services.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _db;

    protected Repository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public T GetById(int id)
    {
        return _db.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _db.Set<T>().ToList();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _db.Set<T>().Where(expression);
    }

    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _db.Set<T>().AddRange(entities);
    }

    public void Remove(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _db.Set<T>().Update(entity);
    }

    public int SaveChanges()
    {
        return _db.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}