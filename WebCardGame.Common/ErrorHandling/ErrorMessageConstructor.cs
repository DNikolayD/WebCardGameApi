namespace WebCardGame.Common.ErrorHandling
{
    public static class ErrorMessageConstructor
    {

        private static string _errorMessage = String.Empty;

        public static string ConstructErrorMessage(ErrorType errorType, string originClass, string originProperty, params int?[] values)
        {
            string origin = originClass + " " + originProperty;
            switch (errorType)
            {
                case ErrorType.Empty:
                    _errorMessage = origin + " is required!";
                    break;
                case ErrorType.TooShort:
                    _errorMessage = origin + " must be no shorter than " + values[0].Value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooLong:
                    _errorMessage = origin + " must be no longer than " + values[0].Value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooBig:
                    _errorMessage = origin + " must be no bigger than " + values[0].Value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooSmall:
                    _errorMessage = origin + " must be no shorter than " + values[0].Value + " symbols! Please choose another " + originProperty + "!";
                    break;
                default:
                    break;
            }
            return _errorMessage;
        }
    }
}
