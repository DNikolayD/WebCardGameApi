using Moq;
using NUnit.Framework;
using WebCardGame.Data;

namespace WebCardGame.Test.RepositoryTests.DeletableRepositoryTests
{
    public class DeletableRepositoryTests
    {
        private Mock<ApplicationDbContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<ApplicationDbContext>();
        }

        [Test]
        public void TestingAllGetMethodReturningSuccess()
        {

        }
    }
}
