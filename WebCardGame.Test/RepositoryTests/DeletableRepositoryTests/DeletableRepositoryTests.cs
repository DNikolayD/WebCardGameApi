using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using WebCardGame.Common.Logger;
using WebCardGame.Data;
using WebCardGame.Data.Repositories;

namespace WebCardGame.Test.RepositoryTests.DeletableRepositoryTests
{
    public class DeletableRepositoryTests : BaseTest
    {/*
        private Mock<ApplicationDbContext> _context;
        private AbstractValidator<object> _validator;
        private IDeletableRepository<object> _deletableRepository;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<ApplicationDbContext>();
            _validator = new InlineValidator<object>();
            _deletableRepository = new DeletableRepository<object>(
                _context.Object,
                _validator,
                new NullLogger<DeletableRepository<object>>()
            );
        }

        [Test]
        public async Task DeletableRepository_GetAllAsync_ReturnsSuccess()
        {
            var table = _fixture.CreateMany<object>(5);
            _context.Setup(x => x.Set<object>()).Returns(table as DbSet<object>);
            _validator.RuleFor(x => x.GetType());
            var result = await _deletableRepository.GetAllAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("This response was created on 16 from DeletableRepository, GetAllAsync. The response contains no errors and it has payload of type object", result.GetMessage());
        }
        */
    }
}
