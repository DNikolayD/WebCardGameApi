﻿using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.Repositories
{
    public interface IDeletableRepository<T> where T : class, IDeletableDataEntity<object>
    {
        IQueryable<T> GetAllAsync();

        Task<IQueryable<T>> GetByIdAsync(object id);

        Task<IQueryable<T>> InsertAsync(T obj);

        IQueryable<T> Update(T obj);

        Task<bool> DeleteAsync(object id);

        Task SaveAsync();

        Task<IQueryable<T>> FillterAsync(string propertyName, object value);
    }
}
