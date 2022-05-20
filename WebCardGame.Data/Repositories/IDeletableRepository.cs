using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.Requests;
using WebCardGame.Data.Responses;

namespace WebCardGame.Data.Repositories
{
    public interface IDeletableRepository<T> where T : class, IDeletableDataEntity<object>
    {
        Task<BaseDataResponse> GetAllAsync();

        Task<BaseDataResponse> GetByIdAsync(BaseDataRequest request);

        Task<BaseDataResponse> InsertAsync(BaseDataRequest request);

        Task<BaseDataResponse> UpdateAsync(BaseDataRequest request);

        Task<BaseDataResponse> DeleteAsync(BaseDataRequest request);

        Task<BaseDataResponse> SaveAsync();

        Task<BaseDataResponse> FilterAsync(BaseDataRequest request);
    }
}
