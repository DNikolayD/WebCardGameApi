using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCardGame.Common;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Service.DTOs;
using WebCardGame.Service.DTOs.CardDtos;

namespace WebCardGame.Service.Services
{
    public class CardService : ICardService
    {
        private readonly IDeletableRepository<CardDataEntity> _repository;

        public CardService(IDeletableRepository<CardDataEntity> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(FullCardDto fullCardDto)
        {
            CardDataEntity cardDataEntity = (CardDataEntity)fullCardDto.MapTo(new CardDataEntity().GetType());
            await _repository.InsertAsync(cardDataEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
