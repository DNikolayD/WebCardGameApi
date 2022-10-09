using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.Testing;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Service.DTOs.CardDTOs;
using WebCardGame.Service.Requests;
using WebCardGame.Service.Services.Cards;
using static WebCardGame.Common.DataConstraints.CardDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;

namespace WebCardGame.Test.ServiceTests
{
    public class CardServiceTests : BaseTest
    {
        private Mock<IRepository<CardDataEntity>> _repository;

        private CardService _cardService;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<CardDataEntity>>();
            _cardService = new CardService(_repository.Object, new NullLogger<CardService>());
        }

        [Test]
        public async Task AddAsync_ReturnsSuccessfulResponse()
        {
            const string origin = "CardController AddCard";
            const string type = "Add";
            var cardId = _fixture.Create<string>();
            var cardName = _fixture.GenerateRandomString(MinNameLength, MaxNameLength);
            var cardDescription = _fixture.GenerateRandomString(MinDescriptionLength, MaxDescriptionLength);
            var cardCost = _fixture.GenerateRandomInt(MinCostValue, MaxCostValue);
            var cardImageId = _fixture.Create<string>();
            var cardTypeId = _fixture.GenerateRandomInt(0, Int32.MaxValue);
            var numberOfCardEffectIds = _fixture.Create<int>();
            var cardEffectIds = _fixture.CreateMany<string>(numberOfCardEffectIds).ToArray();
            var cardCreatorId = _fixture.Create<string>();
            var payload = _fixture.Build<FullCardDto>()
                .With(x => x.Id, cardId)
                .With(x => x.Name, cardName)
                .With(x => x.Description, cardDescription)
                .With(x => x.Cost, cardCost)
                .With(x => x.ImageId, cardImageId)
                .With(x => x.TypeId, cardTypeId)
                .With(x => x.EffectIds, cardEffectIds)
                .With(x => x.CreatorId, cardCreatorId)
                .Create();

            var request = _fixture.Build<BaseDtoRequest>()
                .With(x => x.Origin, origin)
                .With(x => x.Type, type)
                .With(x => x.Payload, payload)
                .Create();

           var response = await _cardService.AddAsync(request);

           Assert.IsNotNull(response);
           Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task AddAsync_ReturnsResponseWithOriginErrorMessage()
        {
            const string origin = "";
            const string type = "Add";
            var errorMessage = $"{origin} is not a valid origin for {type}";
            var cardId = _fixture.Create<string>();
            var cardName = _fixture.GenerateRandomString(MinNameLength, MaxNameLength);
            var cardDescription = _fixture.GenerateRandomString(MinDescriptionLength, MaxDescriptionLength);
            var cardCost = _fixture.GenerateRandomInt(MinCostValue, MaxCostValue);
            var cardImageId = _fixture.Create<string>();
            var cardTypeId = _fixture.GenerateRandomInt(0, Int32.MaxValue);
            var numberOfCardEffectIds = _fixture.Create<int>();
            var cardEffectIds = _fixture.CreateMany<string>(numberOfCardEffectIds).ToArray();
            var cardCreatorId = _fixture.Create<string>();
            var payload = _fixture.Build<FullCardDto>()
                .With(x => x.Id, cardId)
                .With(x => x.Name, cardName)
                .With(x => x.Description, cardDescription)
                .With(x => x.Cost, cardCost)
                .With(x => x.ImageId, cardImageId)
                .With(x => x.TypeId, cardTypeId)
                .With(x => x.EffectIds, cardEffectIds)
                .With(x => x.CreatorId, cardCreatorId)
                .Create();

            var request = _fixture.Build<BaseDtoRequest>()
                .With(x => x.Origin, origin)
                .With(x => x.Type, type)
                .With(x => x.Payload, payload)
                .Create();

            var response = await _cardService.AddAsync(request);

            Assert.IsNotNull(response);
            Assert.IsTrue(!response.IsSuccessful);
            Assert.AreEqual(errorMessage, response.Errors.FirstOrDefault());
        }

    }
}
