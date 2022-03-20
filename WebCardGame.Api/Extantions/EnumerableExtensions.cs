namespace WebCardGame.Api.Extantions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();

    }
}
