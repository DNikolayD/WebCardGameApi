using WebCardGame.Common;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Data.Requests;
using WebCardGame.Service.DTOs.CardDtos;
using WebCardGame.Service.Requests;

namespace WebCardGame.Service.Services
{
    public class CardService : ICardService
    {
        private readonly Type _dataEntityType;

        private readonly Type _fullCardDtoType;

        private readonly IDeletableRepository<CardDataEntity> _repository;

        public CardService(IDeletableRepository<CardDataEntity> repository)
        {
            _repository = repository;
            _dataEntityType = typeof(CardDataEntity);
            _fullCardDtoType = typeof(CardDataEntity);
        }

        public async Task AddAsync(FullCardDto fullCardDto)
        {
            var request = new BaseDtoRequest()
            {
                Origin = "CardService AddAsync",
                Payload = fullCardDto,
                Type = "Insert"
            };
            await _repository.InsertAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var request = new BaseDtoRequest()
            {
                Origin = "CardService DeleteAsync",
                Payload = id,
                Type = "Delete"
            };
            await _repository.DeleteAsync((BaseDataRequest)request.MapTo(typeof(BaseDataRequest)));
            await _repository.SaveAsync();
        }

        public async Task<FullCardDto> GetAsync(string id)
        {
            return null; //(FullCardDto)(await _repository.GetByIdAsync(id)).FirstOrDefault().MapTo(_fullCardDtoType);
        }

        public async Task<FullCardDto> UpdateAsync(FullCardDto fullCardDto)
        {
            var cardDataEntity = (CardDataEntity)fullCardDto.MapTo(_dataEntityType);
            //_repository.UpdateAsync(cardDataEntity);
            await _repository.SaveAsync();
            return fullCardDto;
        }

        public List<FullCardDto> GetAllUnFilteredAsync()
        {
            //List<FullCardDto> fullCards = _repository.GetAllAsync().Select(x => (FullCardDto)x.MapTo(_fullCardDtoType)).ToList();
            return null; //fullCards;
        }


        public async Task<List<FullCardDto>> GetAllFillteredAsync(string propertyName, object value)
        {
            //List<FullCardDto> fullCards = (await _repository.FilterAsync(propertyName, value)).Select(x => (FullCardDto)x.MapTo(_fullCardDtoType)).ToList();
            return null; //fullCards;
        }

    }
}
