using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.Repositories
{
    public interface IRepository<T> where T : class, IDataEntity
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