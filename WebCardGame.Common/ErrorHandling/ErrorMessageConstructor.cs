using WebCardGame.Common.ValidationModels;

namespace WebCardGame.Common.ErrorHandling
{
    public static class ErrorMessageConstructor
    {

        private static string _errorMessage = String.Empty;

        public static string ConstructErrorMessage(this BaseValidationModel baseValidationModel)
        {
            var errorType = baseValidationModel.ErrorType;
            var originClass = baseValidationModel.OriginClass;
            var originProperty = baseValidationModel.OriginProperty;
            var value = baseValidationModel.Value;

            var origin = originClass + " " + originProperty;
            switch (errorType)
            {
                case ErrorType.Empty:
                    _errorMessage = origin + " is required!";
                    break;
                case ErrorType.TooShort:
                    _errorMessage = origin + " must be no shorter than " + value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooLong:
                    _errorMessage = origin + " must be no longer than " + value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooBig:
                    _errorMessage = origin + " must be no bigger than " + value + " symbols! Please choose another " + originProperty + "!";
                    break;
                case ErrorType.TooSmall:
                    _errorMessage = origin + " must be no shorter than " + value + " symbols! Please choose another " + originProperty + "!";
                    break;
                default:
                    break;
            }
            return _errorMessage;
        }
    }
}
