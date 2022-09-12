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
            _errorMessage = errorType switch
            {
                ErrorType.Empty => origin + " is required!",
                ErrorType.TooShort => origin + " must be no shorter than " + value +
                                      " symbols! Please choose another " + originProperty + "!",
                ErrorType.TooLong => origin + " must be no longer than " + value + " symbols! Please choose another " +
                                     originProperty + "!",
                ErrorType.TooBig => origin + " must be no bigger than " + value + " symbols! Please choose another " +
                                    originProperty + "!",
                ErrorType.TooSmall => origin + " must be no shorter than " + value +
                                      " symbols! Please choose another " + originProperty + "!",
                ErrorType.WrongOrigin => baseValidationModel.CalledFrom + " is not a valid origin for" + origin,
                ErrorType.WrongType => baseValidationModel.TypeOfCall + "is not a valid call type for" + origin,
                _ => throw new ArgumentOutOfRangeException()
            };
            return _errorMessage;
        }
    }
}
