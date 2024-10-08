namespace MotoApp.Data.Repositories;

using MotoApp.Data.Entities;
using System.Text.Json;

public class ListRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items = new();
    int lastID = 1;
    private readonly string file = $"{typeof(T).Name}.json";
    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public void Add(T item)
    {
        if (_items.Count == 0)
        {
            item.Id = lastID;
            lastID++;
        }
        else if (_items.Count > 0)
        {
            lastID = _items[_items.Count - 1].Id;
            item.Id = ++lastID;
        }
        _items.Add(item);
        ItemAdded.Invoke(this, item);
    }

    public T GetById(int id)
    {
        return _items.Single(item => item.Id == id);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved.Invoke(this, item);
    }

    public void Save()
    {
        File.Delete(file);
        string jsonString = JsonSerializer.Serialize<IEnumerable<T>>(_items);
        File.WriteAllText(file, jsonString);
    }

    public IEnumerable<T> ItemsToList()
    {
        if (_items.Count == 0)
        {
            if (File.Exists(file))
            {
                var serializedItems = File.ReadAllText(file);
                var deserializedItems = JsonSerializer.Deserialize<IEnumerable<T>>(serializedItems);
                if (deserializedItems != null)
                {
                    foreach (var item in deserializedItems)
                    {
                        _items.Add(item);
                    }
                }
            }
        }
        return _items;
    }
}

