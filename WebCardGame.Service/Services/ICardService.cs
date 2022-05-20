using WebCardGame.Service.DTOs.CardDtos;
using WebCardGame.Service.InjectionTypes;

namespace WebCardGame.Service.Services
{
    public interface ICardService : IService
    {
        public Task AddAsync(FullCardDto fullCardDto);
    }
}
