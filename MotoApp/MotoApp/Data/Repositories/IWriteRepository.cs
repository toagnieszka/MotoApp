namespace MotoApp.Data.Repositories;

using MotoApp.Data.Entities;

public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T item);

    void Save();

    void Remove(T item);
}
