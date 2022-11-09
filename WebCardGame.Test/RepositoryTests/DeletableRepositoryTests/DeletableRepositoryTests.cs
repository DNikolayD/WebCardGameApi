namespace WebCardGame.Test.RepositoryTests.DeletableRepositoryTests
{
    public class DeletableRepositoryTests : BaseTest
    {/*
        private Mock<ApplicationDbContext> _context;
        private AbstractValidator<object> _validator;
        private IRepository<object> _deletableRepository;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<ApplicationDbContext>();
            _validator = new InlineValidator<object>();
            _deletableRepository = new Repository<object>(
                _context.Object,
                _validator,
                new NullLogger<Repository<object>>()
            );
        }

        [Test]
        public async Task DeletableRepository_GetAllAsync_ReturnsSuccess()
        {
            var table = _fixture.CreateMany<object>(5);
            _context.Setup(x => x.Set<object>()).Returns(table as DbSet<object>);
            _validator.RuleFor(x => x.GetType());
            var result = await _deletableRepository.GetAllAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("This response was created on 16 from Repository, GetAllAsync. The response contains no errors and it has payload of type object", result.GetMessage());
        }
        */
    }
}
