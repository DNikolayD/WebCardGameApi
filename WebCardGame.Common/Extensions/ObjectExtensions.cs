namespace WebCardGame.Common.Extensions
{
    public static class ObjectExtensions
    {

        public static bool BeNotNull(this object? obj) => obj != null;

        public static bool BeNoShorter(this object obj, int length) => obj.BeNotNull() && obj.ToString().Length >= length;

        public static bool BeNoLonger(this object obj, int length) => obj.BeNotNull() && obj.ToString().Length <= length;

        public static bool BeNoSmaller(this object obj, int value) => obj.BeNotNull() && int.Parse(obj.ToString()) >= value;

        public static bool BeNoBigger(this object obj, int value) => obj.BeNotNull() && int.Parse(obj.ToString()) <= value;

    }
}
