﻿using WebCardGame.Data.Requests;
using WebCardGame.Data.Responses;

namespace WebCardGame.Data.Repositories
{
    public interface IRepository<T> where T : class
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
