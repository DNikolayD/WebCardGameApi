using FluentValidation;
using WebCardGame.Common.ErrorHandling;
using WebCardGame.Common.ValidationModels;

namespace WebCardGame.Common.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static void SetNotNullRule(
            this IRuleBuilder<IBaseEntity, object> rulePointer, BaseValidationModel baseValidationModel)
        {
            baseValidationModel.ErrorType = ErrorType.Empty;
            var errorMessage = baseValidationModel.ErrorMessage;
            var errorCode = baseValidationModel.ErrorCode;
            var rule = rulePointer.Must(obj => obj.BeNotNull());
            rule.WithMessage(errorMessage).WithErrorCode(errorCode);
        }

        public static void SetTooShortRule(
            this IRuleBuilder<IBaseEntity, object> rulePointer, BaseValidationModel baseValidationModel)
        {
            baseValidationModel.ErrorType = ErrorType.TooShort;
            var length = baseValidationModel.Value;
            var errorMessage = baseValidationModel.ErrorMessage;
            var errorCode = baseValidationModel.ErrorCode;
            var rule = rulePointer.Must(obj => obj.BeNoShorter(length));
            rule.WithMessage(errorMessage).WithErrorCode(errorCode);
        }

        public static void SetTooLongRule(
            this IRuleBuilder<IBaseEntity, object> rulePointer, BaseValidationModel baseValidationModel)
        {
            baseValidationModel.ErrorType = ErrorType.TooLong;
            var length = baseValidationModel.Value;
            var errorMessage = baseValidationModel.ErrorMessage;
            var errorCode = baseValidationModel.ErrorCode;
            var rule = rulePointer.Must(obj => obj.BeNoLonger(length));
            rule.WithMessage(errorMessage).WithErrorCode(errorCode);
        }

        public static void SetTooSmallRule(
            this IRuleBuilder<IBaseEntity, object> rulePointer, BaseValidationModel baseValidationModel)
        {
            baseValidationModel.ErrorType = ErrorType.TooSmall;
            var value = baseValidationModel.Value;
            var errorMessage = baseValidationModel.ErrorMessage;
            var errorCode = baseValidationModel.ErrorCode;
            var rule = rulePointer.Must(obj => obj.BeNoSmaller(value));
            rule.WithMessage(errorMessage).WithErrorCode(errorCode);
        }

        public static void SetTooBigRule(
            this IRuleBuilder<IBaseEntity, object> rulePointer, BaseValidationModel baseValidationModel)
        {
            baseValidationModel.ErrorType = ErrorType.TooBig;
            var value = baseValidationModel.Value;
            var errorMessage = baseValidationModel.ErrorMessage;
            var errorCode = baseValidationModel.ErrorCode;
            var rule = rulePointer.Must(obj => obj.BeNoBigger(value));
            rule.WithMessage(errorMessage).WithErrorCode(errorCode);
        }
    }
}
