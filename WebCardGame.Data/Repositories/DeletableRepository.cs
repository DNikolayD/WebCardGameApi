using Microsoft.EntityFrameworkCore;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.Repositories
{
    public class DeletableRepository<T> : IDeletableRepository<T> where T : class, IDeletableDataEntity<object>
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;

        public DeletableRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
            this._table = this._context.Set<T>();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            IDeletableDataEntity<object> entity = (IDeletableDataEntity<object>)await this._table.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            entity.IsActive = false;
            entity.DeletedOn = DateTime.UtcNow;
            bool result = await this._table.AnyAsync(x => x.IsActive == true && x.Id == id);
            return result;
        }

        public IQueryable<T> GetAllAsync()
        {
            IQueryable<T> result = this._table.Where(x => x.IsActive);
            return result;
        }

        public async Task<IQueryable<T>> GetByIdAsync(object id)
        {
            return (IQueryable<T>)await this._table.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<IQueryable<T>> FillterAsync(string propertyName, object value)
        {
            var all = this.GetAllAsync();
            if ((await all.AnyAsync(x => x.GetType().GetProperty(propertyName) != null)) && (await all.AnyAsync(x => x.GetType().GetProperty(propertyName).GetType() == value.GetType())))
            {
                all = all.Where(x => x.GetType().GetProperty(propertyName).GetValue(x) == value);
            }
            return all;

        }
    }
}
