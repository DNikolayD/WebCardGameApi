using System.ComponentModel.DataAnnotations;
using WebCardGame.Common.Checkers;
using WebCardGame.Common.ErrorHandling;

namespace WebCardGame.Common.CustomValidationAttributes
{
    public class UniversalValidationAttribute : ValidationAttribute
    {
        private readonly string _className;
        private readonly string _propertyName;
        private readonly int? _maxValue;
        private readonly int? _minValue;
        private readonly CheckType _checkType;
        private readonly bool _isString;

        public UniversalValidationAttribute(string className, string propertyName, CheckType checkType, bool isString, params int[] values)
        {
            _className = className;
            _propertyName = propertyName;
            _checkType = checkType;
            _maxValue = values[0];
            _minValue = values[1];
            _isString = isString;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string nullError = string.Empty;
            string maxError = string.Empty;
            string minError = string.Empty;
            switch (_checkType)
            {
                case CheckType.All:
                    nullError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.Empty, _className, _propertyName);
                    if (_isString)
                    {
                        maxError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooLong, _className, _propertyName, _maxValue);
                        minError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooShort, _className, _propertyName, _minValue);
                    }
                    else
                    {
                        maxError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooBig, _className, _propertyName, _maxValue);
                        minError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooSmall, _className, _propertyName, _minValue);
                    }
                    break;
                case CheckType.Empty:
                    nullError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.Empty, _className, _propertyName);
                    break;
                case CheckType.MaxAndMinValue:
                    if (_isString)
                    {
                        maxError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooLong, _className, _propertyName, _maxValue);
                        minError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooShort, _className, _propertyName, _minValue);
                    }
                    else
                    {
                        maxError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooBig, _className, _propertyName, _maxValue);
                        minError = ErrorMessageConstructor.ConstructErrorMessage(ErrorType.TooSmall, _className, _propertyName, _minValue);
                    }
                    break;
                default:
                    break;

            }
            if (((int)_checkType) == 0)
            {
                if (_isString)
                {
                    return ValidationChecker.ValidateNonNullableString(value, _maxValue.GetValueOrDefault(), _minValue.GetValueOrDefault(), nullError, maxError, minError);
                }
                else
                {
                    return ValidationChecker.ValidateNonNullableInt(value, _maxValue.GetValueOrDefault(), _minValue.GetValueOrDefault(), nullError, maxError, minError);
                }
            }
            else if (((int)_checkType) == 2)
            {
                if (_isString)
                {
                    return ValidationChecker.ValidateNullableString(value, _maxValue.GetValueOrDefault(), _minValue.GetValueOrDefault(), maxError, minError);
                }
                else
                {
                    return ValidationChecker.ValidateNullableInt(value, _maxValue.GetValueOrDefault(), _minValue.GetValueOrDefault(), maxError, minError);
                }
            }
            else
            {
                return ValidationChecker.RequiredObject(value, nullError);
            }
        }
    }
}