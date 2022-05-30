using WebCardGame.Service.InjectionTypes;
using WebCardGame.Service.Requests;
using WebCardGame.Service.Responses;

namespace WebCardGame.Service.Services
{
    public interface ICardService : IService
    {
        public Task<BaseDtoResponse> AddAsync(BaseDtoRequest request);

        public Task<BaseDtoResponse> UpdateAsync(BaseDtoRequest request);

        public Task<BaseDtoResponse> DeleteAsync(BaseDtoRequest request);

        public Task<BaseDtoResponse> GetByIdAsync(BaseDtoRequest request);

        public Task<BaseDtoResponse> GetAllAsync(BaseDtoRequest request);
    }
}
