namespace WebCardGame.Common.Builders
{
    public static class OriginBuilder
    {
        public static string Origin(string className, string propertyName)
        {
            return String.Join(" ", new List<string?>()
            {
                className,
                propertyName
            });
        }
    }
}
