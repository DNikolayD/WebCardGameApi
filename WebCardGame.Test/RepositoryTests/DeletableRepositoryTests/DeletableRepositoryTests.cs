using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
