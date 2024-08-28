﻿namespace MotoApp.Repositories;

using MotoApp.Entities;


public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    event EventHandler<T?>? ItemAdded;
    event EventHandler<T?>? ItemRemoved;
}

