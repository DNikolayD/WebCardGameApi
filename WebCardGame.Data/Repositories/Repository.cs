using Microsoft.EntityFrameworkCore;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, INonDeletableDataEntity<object>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
            this._table = this._context.Set<T>();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = (T)await this.GetByIdAsync(id);
            entity.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead && p.Name.EndsWith("Id") && p.Name != "Id").ToList().ForEach(p => p.SetValue(p, null));
            this._table.Remove(entity);
            return (await _table.FindAsync(id)) != null;
        }

        public async Task<IQueryable<T>> FillterAsync(string propertyName, object value)
        {
            var all = this.GetAllAsync();
            if ((await all.AnyAsync(x => x.GetType().GetProperty(propertyName) != null)) && (await all.AnyAsync(x => x.GetType().GetProperty(propertyName).GetType() == value.GetType())))
            {
                all = all.Where(x => x.GetType().GetProperty(propertyName).GetValue(x) == value);
            }
            return all;
        }

        public IQueryable<T> GetAllAsync()
        {
            IQueryable<T> result = this._table;
            return result;
        }

        public async Task<IQueryable<T>> GetByIdAsync(object id)
        {
            IQueryable<T> result = (IQueryable<T>)await this._table.FindAsync(id);
            return result;
        }

        public async Task<IQueryable<T>> InsertAsync(T obj)
        {
            await this._table.AddAsync(obj);
            return (IQueryable<T>)obj;
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public IQueryable<T> Update(T obj)
        {
            this._table.Attach(obj);
            this._context.Entry(obj).State = EntityState.Modified;
            return (IQueryable<T>)obj;
        }
    }
}
