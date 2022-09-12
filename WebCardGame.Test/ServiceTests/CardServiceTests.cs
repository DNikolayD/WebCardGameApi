using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using WebCardGame.Common.Extensions;
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
            var cardName = _fixture.Create<string>();
            var cardDescription = _fixture.Create<string>();
            var cardCost = _fixture.Create<int>();
            var cardImageId = _fixture.Create<string>();
            var cardTypeId = _fixture.Create<int>();
            var numberOfCardEffectIds = _fixture.Create<int>();
            var cardEffectIds = _fixture.CreateMany<string>(numberOfCardEffectIds).ToArray();
            var cardCreatorId = _fixture.Create<string>();
            while (cardName.BeNoShorter(MinNameLength) && cardName.BeNoLonger(MaxDescriptionLength))
            {
                cardName = _fixture.Create<string>();
            }

            while (cardDescription.BeNoShorter(MinDescriptionLength) && cardDescription.BeNoLonger(MaxDescriptionLength))
            {
                cardDescription = _fixture.Create<string>();
            }

            while (!(cardCost.BeNoSmaller(MinCostValue) && cardCost.BeNoBigger(MaxCostValue)))
            {
                cardCost = _fixture.Create<int>();
            }

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
           Assert.AreEqual(true, response.IsSuccessful);
        }

    }
}
