﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebCardGame.Common;
using WebCardGame.Common.Logger;
using WebCardGame.Common.Requests;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Data.Requests;
using WebCardGame.Data.Responses;

namespace WebCardGame.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BaseValidator _validator;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(ApplicationDbContext applicationDbContext, ILogger<Repository<T>> logger)
        {
            var baseValidationModel = new BaseValidationModel($"Repository for {typeof(T).Name}");
            _validator = new BaseValidator(baseValidationModel);
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _logger = logger;
            _table = _context.Set<T>();
        }


        public async Task<BaseDataResponse> GetAllAsync()
        {
            var response = new BaseDataResponse()
            {
                Origin = "Repository, GetAllAsync"
            };
            var entities = await _table.ToListAsync();
            foreach (var entity in entities)
            {
                var validEntities = new List<T>();
                _validator.Validate(entity);
                if (_validator.Errors.Any())
                {
                    response.Payload = String.Empty;
                    response.Errors.AddRange(_validator.Errors);
                }
            }

            response.IsSuccessful = !response.Errors.Any();
            if (response.IsSuccessful)
            {
                response.Payload = entities;
            }
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> GetByIdAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = await _table.FindAsync(request.Payload);
            _validator.Validate(entity);
            if (_validator.Errors.Any())
            {
                response.Errors.AddRange(_validator.Errors);
            }
            response.IsSuccessful = !response.Errors.Any();
            if (response.IsSuccessful)
            {
                response.Payload = entity;
            }
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> InsertAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)request.Payload.MapTo(typeof(T));
            _validator.Validate(entity);
            if (_validator.Errors.Any())
            {
                response.Errors.AddRange(_validator.Errors);
            }

            response.IsSuccessful = !response.Errors.Any();
            if (response.IsSuccessful)
            {
                await _table.AddAsync(entity);
                response.Payload = _table.Contains(entity);
            }
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> UpdateAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)request.Payload.MapTo(typeof(T));
            _validator.Validate(entity);
            if (_validator.Errors.Any())
            {
                response.Errors.AddRange(_validator.Errors);
            }

            response.IsSuccessful = !response.Errors.Any();
            if (response.IsSuccessful)
            {
                _table.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                response.Payload = _table.Contains(entity);
            }
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> DeleteAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var entity = (T)(await GetByIdAsync(request)).Payload;
            entity.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead && p.Name.EndsWith("Id") && p.Name != "Id").ToList().ForEach(p => p.SetValue(p, null));
            _table.Remove(entity);
            response.IsSuccessful = !_table.Contains(entity);
            response.Payload = !_table.Contains(entity);
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> SaveAsync()
        {
            var response = new BaseDataResponse();
            var changes = await _context.SaveChangesAsync();
            response.IsSuccessful = changes > 0;
            response.Payload = changes;
            _logger.LogInformation(response.GetMessage());
            return response;
        }

        public async Task<BaseDataResponse> FilterAsync(BaseDataRequest request)
        {
            var response = new BaseDataResponse();
            var payload = request.Payload.MapTo(typeof(FilteringObject));
            var filter = (FilteringObject)payload;
            var propertyName = filter.PropertyName;
            var value = filter.Value;
            var all = (await GetAllAsync()).Payload as List<T>;
            if (all.Exists(x => x.GetType().GetProperty(propertyName) != null) && all.Exists(x => x.GetType().GetProperty(propertyName).GetType() == value.GetType()))
            {
                all = all.FindAll(x => x.GetType().GetProperty(propertyName).GetValue(x) == value);
            }
            response.IsSuccessful = true;
            response.Payload = all;
            _logger.LogInformation(response.GetMessage());
            return response;
        }
    }
}
