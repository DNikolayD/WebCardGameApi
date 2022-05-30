using WebCardGame.Common;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Data.Requests;
using WebCardGame.Service.DTOs.CardDTOs;
using WebCardGame.Service.Requests;
using WebCardGame.Service.Responses;
using WebCardGame.Service.Validators;

namespace WebCardGame.Service.Services
{
    public class CardService : ICardService
    {

        private readonly IDeletableRepository<CardDataEntity> _repository;

        private readonly FullCardDTOValidator _validator;

        public CardService(IDeletableRepository<CardDataEntity> repository, FullCardDTOValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<BaseDtoResponse> AddAsync(BaseDtoRequest request)
        {
            var baseDtoResponse = new BaseDtoResponse();
            var payload = (FullCardDto)request.Payload.MapTo(typeof(FullCardDto));
            await _repository.InsertAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
            await _repository.SaveAsync();
            var validatedPayload = await _validator.ValidateAsync(payload);
            baseDtoResponse.Errors = validatedPayload.Errors.Select(e => e.ErrorMessage + e.ErrorCode).ToList();
            baseDtoResponse.IsSuccess = !baseDtoResponse.Errors.Any();
            if (baseDtoResponse.IsSuccess)
            {
                baseDtoResponse.Payload = validatedPayload;
            }
            return baseDtoResponse;
        }

        public async Task<BaseDtoResponse> UpdateAsync(BaseDtoRequest request)
        {
            var baseDtoResponse = new BaseDtoResponse();
            var payload = (FullCardDto)request.Payload.MapTo(typeof(FullCardDto));
            await _repository.UpdateAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
            await _repository.SaveAsync();
            var validatedPayload = await _validator.ValidateAsync(payload);
            baseDtoResponse.Errors = validatedPayload.Errors.Select(e => e.ErrorMessage + e.ErrorCode).ToList();
            baseDtoResponse.IsSuccess = !baseDtoResponse.Errors.Any();
            if (baseDtoResponse.IsSuccess)
            {
                baseDtoResponse.Payload = validatedPayload;
            }
            return baseDtoResponse;
        }

        public async Task<BaseDtoResponse> DeleteAsync(BaseDtoRequest request)
        {
            var baseDtoResponse = new BaseDtoResponse();
            var payload = (FullCardDto)request.Payload.MapTo(typeof(FullCardDto));
            await _repository.DeleteAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
            await _repository.SaveAsync();
            baseDtoResponse.IsSuccess = (await _repository.GetAllAsync()).IsSuccess;
            if (baseDtoResponse.IsSuccess)
            {
                baseDtoResponse.Payload = payload;
            }
            return baseDtoResponse;
        }

        public async Task<BaseDtoResponse> GetByIdAsync(BaseDtoRequest request)
        {
            var baseDtoResponse = new BaseDtoResponse();
            var dataRequest = (BaseDataRequest)request.MapTo(typeof(BaseDataRequest));
            var responsePayload = (FullCardDto)(await _repository.GetByIdAsync(dataRequest)).Payload.MapTo(typeof(FullCardDto));
            var validatedPayload = await _validator.ValidateAsync(responsePayload);
            baseDtoResponse.Errors = validatedPayload.Errors.Select(e => e.ErrorMessage + e.ErrorCode).ToList();
            baseDtoResponse.IsSuccess = !baseDtoResponse.Errors.Any();
            if (baseDtoResponse.IsSuccess)
            {
                baseDtoResponse.Payload = validatedPayload;
            }

            return baseDtoResponse;
        }

        public async Task<BaseDtoResponse> GetAllAsync(BaseDtoRequest request)
        {
            var baseDtoResponse = new BaseDtoResponse();
            var dataRequest = (BaseDataRequest)request.MapTo(typeof(BaseDataRequest));
            var responsePayload = (List<FullCardDto>)(await _repository.GetByIdAsync(dataRequest)).Payload.MapTo(typeof(List<FullCardDto>));
            var validatedPayload = responsePayload.Select(rp => _validator.Validate(rp));
            baseDtoResponse.Errors =
                (List<string>)validatedPayload.Select(vp => vp.Errors.Select(e => e.ErrorMessage + e.ErrorCode));
            baseDtoResponse.IsSuccess = !baseDtoResponse.Errors.Any();
            if (baseDtoResponse.IsSuccess)
            {
                baseDtoResponse.Payload = validatedPayload;
            }

            return baseDtoResponse;
        }
    }
}
