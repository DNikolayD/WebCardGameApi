using AutoFixture;
using NUnit.Framework;

namespace WebCardGame.Test
{
    public class BaseTest
    {
        protected Fixture _fixture;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.RepeatCount = default;
        }
    }
}
