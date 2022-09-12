using WebCardGame.Common.ErrorHandling;
using WebCardGame.Common.ValidationModels;

namespace WebCardGame.Common.Extensions
{
    public static class ValidationModelExtension
    {
        public static void SetNotNullRule(
            this BaseValidationModel baseValidationModel, object obj)
        {
            if (obj.BeNotNull())
            {
                return;
            }

            baseValidationModel.ErrorType = ErrorType.Empty;
            baseValidationModel.ErrorCode = "402 Invalid input";
            baseValidationModel.ErrorMessage = baseValidationModel.ConstructErrorMessage();
        }

        public static void SetTooShortRule(
            this BaseValidationModel baseValidationModel, object obj)
        {
            var value = baseValidationModel.Value;
            if (obj.BeNoShorter(value))
            {
                return;
            }

            baseValidationModel.ErrorType = ErrorType.TooShort;
            baseValidationModel.ErrorCode = "402 Invalid input";
            baseValidationModel.ErrorMessage = baseValidationModel.ConstructErrorMessage();
        }

        public static void SetTooLongRule(
            this BaseValidationModel baseValidationModel, object obj)
        {
            var value = baseValidationModel.Value;
            if (obj.BeNoLonger(value))
            {
                return;
            }

            baseValidationModel.ErrorType = ErrorType.TooLong;
            baseValidationModel.ErrorCode = "402 Invalid input";
            baseValidationModel.ErrorMessage = baseValidationModel.ConstructErrorMessage();
        }

        public static void SetTooSmallRule(
            this BaseValidationModel baseValidationModel, object obj)
        {
            var value = baseValidationModel.Value;
            if (obj.BeNoSmaller(value))
            {
                return;
            }

            baseValidationModel.ErrorType = ErrorType.TooSmall;
            baseValidationModel.ErrorCode = "402 Invalid input";
            baseValidationModel.ErrorMessage = baseValidationModel.ConstructErrorMessage();
        }

        public static void SetTooBigRule(
            this BaseValidationModel baseValidationModel, object obj)
        {
            var value = baseValidationModel.Value;
            if (obj.BeNoBigger(value))
            {
                return;
            }

            baseValidationModel.ErrorType = ErrorType.TooBig;
            baseValidationModel.ErrorCode = "402 Invalid input";
            baseValidationModel.ErrorMessage = baseValidationModel.ConstructErrorMessage();
        }
    }
}
