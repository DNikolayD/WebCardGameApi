using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebCardGame.Common;
using WebCardGame.Common.Requests;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.Requests;
using WebCardGame.Data.Responses;

namespace WebCardGame.Data.Repositories
{
    public class DeletableRepository<T> : IDeletableRepository<T> where T : class, IDeletableDataEntity<object>
    {
        private readonly AbstractValidator<T> _validator;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;

        public DeletableRepository(ApplicationDbContext applicationDbContext, AbstractValidator<T> validation)
        {
            _validator = validation ?? throw new ArgumentNullException(nameof(validation));
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this._table = _context.Set<T>();
        }


        public async Task<BaseDataResponse> GetAllAsync()
        {
            var response = new BaseDataResponse();
            var entities = await _table.ToListAsync();
            foreach (var result in entities.Select(entity => _validator.Validate(entity)))
            {
                foreach (var error in result.Errors.Select(x => x.ErrorMessage + x.ErrorCode))
                {
                    response.Errors.Add(error);
                }
            }
            response.IsSuccess = !response.Errors.Any();
            if (response.IsSuccess)
            {
                response.Payload = entities;
            }
            return response;
        }

        public async Task<BaseDataResponse> GetByIdAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = await _table.FirstOrDefaultAsync(x => x.Id == request.Payload);
            var validationResult = await _validator.ValidateAsync(entity);
            foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage + x.ErrorCode))
            {
                response.Errors.Add(error);
            }
            response.IsSuccess = !response.Errors.Any();
            if (response.IsSuccess)
            {
                response.Payload = entity;
            }
            return response;
        }

        public async Task<BaseDataResponse> InsertAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)request.Payload.MapTo(typeof(T));
            var validationResult = await _validator.ValidateAsync(entity);
            foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage + x.ErrorCode))
            {
                response.Errors.Add(error);
            }

            response.IsSuccess = !response.Errors.Any();
            if (response.IsSuccess)
            {
                await _table.AddAsync(entity);
                response.Payload = _table.Contains(entity);
            }

            return response;
        }

        public async Task<BaseDataResponse> UpdateAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)request.Payload.MapTo(typeof(T));
            var validationResult = await _validator.ValidateAsync(entity);
            foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage + x.ErrorCode))
            {
                response.Errors.Add(error);
            }

            response.IsSuccess = !response.Errors.Any();
            if (response.IsSuccess)
            {
                _table.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                response.Payload = _table.Contains(entity);
            }

            return response;
        }

        public async Task<BaseDataResponse> DeleteAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)(await this.GetByIdAsync(request)).Payload;
            entity.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead && p.Name.EndsWith("Id") && p.Name != "Id").ToList().ForEach(p => p.SetValue(p, null));
            this._table.Remove(entity);
            response.IsSuccess = !_table.Contains(entity);
            response.Payload = !_table.Contains(entity);
            return response;
        }

        public async Task<BaseDataResponse> SaveAsync()
        {
            var response = new BaseDataResponse();
            var changes = await _context.SaveChangesAsync();
            response.IsSuccess = changes > 0;
            response.Payload = changes;
            return response;
        }

        public async Task<BaseDataResponse> FilterAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var payload = request.Payload.MapTo(typeof(FilteringObject));
            var filter = (FilteringObject)payload;
            var propertyName = filter.PropertyName;
            var value = filter.Value;
            var all = (await this.GetAllAsync()).Payload as List<T>;
            if (all.Exists(x => x.GetType().GetProperty(propertyName) != null) && all.Exists(x => x.GetType().GetProperty(propertyName).GetType() == value.GetType()))
            {
                all = all.FindAll(x => x.GetType().GetProperty(propertyName).GetValue(x) == value);
            }

            response.IsSuccess = true;
            response.Payload = all;
            return response;
        }
    }
}
