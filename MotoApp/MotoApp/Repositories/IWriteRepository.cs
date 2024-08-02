namespace MotoApp.Repositories;

using MotoApp.Entities;


public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T item);

    void Save();

    void Remove(T item);
}
