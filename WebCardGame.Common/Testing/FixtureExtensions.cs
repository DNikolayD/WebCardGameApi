using AutoFixture;

namespace WebCardGame.Common.Testing
{
    public static class FixtureExtensions
    {
        private static readonly Random _random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public static string GenerateRandomString(this Fixture fixture, int minLength, int maxLength)
        {
            var length = _random.Next(minLength, maxLength);
            return RandomString(length);
        }

        public static List<string> GenerateRandomStrings(this Fixture fixture, int minLength, int maxLength, int numberOfStrings)
        {
            var strings = new List<string>();
            for (var i = 0; i < numberOfStrings; i++)
            {
                var length = _random.Next(minLength, maxLength);
                var randomString = RandomString(length);
                strings.Add(randomString);
            }

            return strings;
        }

        public static int GenerateRandomInt(this Fixture fixture, int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
