using Microsoft.Extensions.Logging;
using WebCardGame.Common;
using WebCardGame.Common.Builders;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.Logger;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Data.Requests;
using WebCardGame.Service.DTOs;
using WebCardGame.Service.DTOs.CardDTOs;
using WebCardGame.Service.Requests;
using WebCardGame.Service.Responses;

namespace WebCardGame.Service.Services.Cards;

public class CardService : ICardService
{
    private readonly IRepository<CardDataEntity> _repository;

    private readonly BaseValidator _validator;

    private readonly ILogger<CardService> _logger;

    private readonly string _className;

    public CardService(IRepository<CardDataEntity> repository, ILogger<CardService> logger)
    {
        var baseValidationModel = new BaseValidationModel(_className);
        _repository = repository;
        _validator = new BaseValidator(baseValidationModel);
        _logger = logger;
        _className = nameof(GetType);
    }

    public async Task<BaseDtoResponse> AddAsync(BaseDtoRequest request)
    {
        var dtoToMap = ResponseBuilder.BuildBaseResponse(request.Origin, request.Type, request);
        var baseDtoResponse = dtoToMap.MapTo(typeof(BaseDtoResponse)) as BaseDtoResponse;
        baseDtoResponse.Errors = dtoToMap.Errors;
        if (baseDtoResponse.Errors.Any())
        {
            baseDtoResponse.IsSuccessful = false;
            return baseDtoResponse;
        }
        var payload = request.Payload;
        await _repository.InsertAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
        await _repository.SaveAsync();
        _validator.Validate(payload);
        var errors = _validator.Errors.ToHashSet();
        errors.RemoveWhere(x => !x.BeNotNull());
        baseDtoResponse.Errors = errors.ToList();
        baseDtoResponse.IsSuccessful = !baseDtoResponse.Errors.Any();
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? payload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> UpdateAsync(BaseDtoRequest request)
    {
        const string propertyName = nameof(UpdateAsync);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var payload = (FullCardDto)request.Payload.MapTo(typeof(FullCardDto));
        await _repository.UpdateAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
        await _repository.SaveAsync();
        _validator.Validate(payload);
        baseDtoResponse.Errors = _validator.Errors.ToList();
        baseDtoResponse.IsSuccessful = !baseDtoResponse.Errors.Any();
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? payload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> DeleteAsync(BaseDtoRequest request)
    {
        const string propertyName = nameof(DeleteAsync);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var payload = (FullCardDto)request.Payload.MapTo(typeof(FullCardDto));
        await _repository.DeleteAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
        await _repository.SaveAsync();
        baseDtoResponse.IsSuccessful = (await _repository.GetAllAsync()).IsSuccessful;
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? payload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> GetByIdAsync(BaseDtoRequest request)
    {
        const string propertyName = nameof(GetByIdAsync);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var dataRequest = (BaseDataRequest)request.MapTo(typeof(BaseDataRequest));
        var responsePayload = (FullCardDto)(await _repository.GetByIdAsync(dataRequest)).Payload.MapTo(typeof(FullCardDto));
        _validator.Validate(responsePayload);
        baseDtoResponse.Errors = _validator.Errors.ToList();
        baseDtoResponse.IsSuccessful = !baseDtoResponse.Errors.Any();
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? responsePayload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> GetAllAsync(BaseDtoRequest request)
    {
        const string propertyName = nameof(GetAllAsync);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var responsePayload = (List<FullCardDto>)(await _repository.GetAllAsync()).Payload.MapTo(typeof(List<FullCardDto>));
        responsePayload.ForEach(rp => _validator.Validate(rp));
        baseDtoResponse.Errors = _validator.Errors.ToList();
        baseDtoResponse.IsSuccessful = !baseDtoResponse.Errors.Any();
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? responsePayload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> GetAllByFilter(BaseDtoRequest request)
    {
        const string propertyName = nameof(GetAllByFilter);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var dataRequest = (BaseDataRequest)request.MapTo(typeof(BaseDataRequest));
        var responsePayload = (List<FullCardDto>)(await _repository.GetByIdAsync(dataRequest)).Payload.MapTo(typeof(List<FullCardDto>));
        responsePayload.ForEach(rp => _validator.Validate(rp));
        baseDtoResponse.Errors = _validator.Errors.ToList();
        baseDtoResponse.IsSuccessful = !baseDtoResponse.Errors.Any();
        baseDtoResponse.Payload = baseDtoResponse.IsSuccessful ? responsePayload : new object();
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }

    public async Task<BaseDtoResponse> SortAllAsync(BaseDtoRequest request)
    {
        const string propertyName = nameof(AddAsync);
        var baseDtoResponse = ResponseBuilder.BuildBaseResponse(_className, propertyName, request) as BaseDtoResponse;
        if (baseDtoResponse.Errors.Any())
        {
            return baseDtoResponse;
        }
        var sortingInfos = (request.Payload as List<object>).Select(x => x.MapTo(typeof(SortingDto)) as SortingDto).ToList();
        var requestForGetAll = new BaseDtoRequest()
        {
            Origin = _className + " " + propertyName,
        };
        var responseFromAll = await this.GetAllAsync(requestForGetAll);
        var cards = (responseFromAll.Payload as List<object>).Select(x => x.MapTo(typeof(FullCardDto)) as FullCardDto).ToList();
        cards = sortingInfos
            .Aggregate(cards, (current, sortingInfo)
                => sortingInfo.IsDescending
                    ? current.OrderByDescending(x => x.GetType()
                    .GetProperty(sortingInfo.PropertyName).Name)
                    .ToList()
                    : current.OrderBy(x => x.GetType()
                        .GetProperty(sortingInfo.PropertyName).Name).ToList());
        baseDtoResponse.Payload = cards;
        _logger.LogInformation(baseDtoResponse.GetMessage());
        return baseDtoResponse;
    }
}