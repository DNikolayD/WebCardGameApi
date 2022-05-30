namespace WebCardGame.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => !source.Any();

    }
}
