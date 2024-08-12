namespace MotoApp.Repositories;

using Microsoft.EntityFrameworkCore;
using MotoApp.Entities;

public delegate void ItemAdded<T>(T item);

public class SqlRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;
    private readonly Action<T>? _itemAddedCallBack;

    public SqlRepository(DbContext dbContext, Action<T>? itemAddedCallBack = null)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _itemAddedCallBack = itemAddedCallBack;
    }

    public event EventHandler<T>? ItemAdded;

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        _itemAddedCallBack?.Invoke(item);
        ItemAdded.Invoke(this, item);

    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
