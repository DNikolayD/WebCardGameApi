using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCardGame.Data;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.Repositories;

namespace WebCardGame.Test.RepositoryTests.RepositoryTests
{
    public class GetAllAsyncTests
    {

        private Mock<ApplicationDbContext> _mockContext;
        private Mock<DbSet<CardDataEntity>> _mockCardDataEntities;

        [SetUp]
        public void SetUp()
        {
            _mockContext = new();
            _mockCardDataEntities = new();
            _mockCardDataEntities.Setup(x => x.AddAsync(It.IsAny<CardDataEntity>(), new CancellationToken()).Result).Callback(() =>
            {
                _mockCardDataEntities.Object.AddAsync(It.IsAny<CardDataEntity>(), new CancellationToken());
            });
            _mockCardDataEntities.Setup(x => x.Remove(It.IsAny<CardDataEntity>())).Callback(() =>
            {
                _mockCardDataEntities.Object.Remove(It.IsAny<CardDataEntity>());
            });
            _mockCardDataEntities.Setup(x => x.Attach(It.IsAny<CardDataEntity>())).Callback(() => 
            {
                _mockCardDataEntities.Object.Attach(It.IsAny<CardDataEntity>());
            });
            //_mockCardDataEntities.Setup(x => x.FindAsync(It.IsAny<Guid>()).Result).Returns(Task.FromResult(new CardDataEntity()).Result);
            _mockContext.Setup(dbContext => dbContext.Set<CardDataEntity>()).Returns(_mockCardDataEntities.Object);
        }

        [Test]
        public async Task GetAsync_ReturnsNull()
        {
            CardDataEntity entity = new CardDataEntity();
            IDeletableRepository<CardDataEntity> repository = new DeletableRepository<CardDataEntity>(_mockContext.Object);
            Assert.IsTrue(repository.GetType() == typeof(DeletableRepository<>));
        }
    }
}
