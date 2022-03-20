using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCardGame.Service.DTOs;
using WebCardGame.Service.DTOs.CardDtos;
using WebCardGame.Service.InjectionTypes;

namespace WebCardGame.Service.Services
{
    public interface ICardService : IService
    {
        public Task AddAsync(FullCardDto fullCardDto);
    }
}
