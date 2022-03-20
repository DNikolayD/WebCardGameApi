using System.ComponentModel.DataAnnotations;

namespace WebCardGame.Common.Checkers
{
    public static class ValidationChecker
    {

        public static ValidationResult ValidateNonNullableString(object? obj, int maxLength, int minLength, string nullObjectError, string maxLengthError, string minLengthError)
        {
            if (CheckIfObjectIsNull(obj))
            {
                return new ValidationResult(nullObjectError);
            }
            string text = (string)obj;
            if (CheckIfStringIsTooLong(text, maxLength))
            {
                return new ValidationResult(maxLengthError);
            }
            if (CheckIfStringIsTooShort(text, minLength))
            {
                return new ValidationResult(minLengthError);
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateNonNullableInt(object? obj, int maxValue, int minValue, string nullObjectError, string maxValueError, string minValueError)
        {
            if (CheckIfObjectIsNull(obj))
            {
                return new ValidationResult(nullObjectError);
            }
            int value = (int)obj;
            if (CheckIfIntIsTooBig(value, maxValue))
            {
                return new ValidationResult(maxValueError);
            }
            if (CheckIfIntIsTooSmall(value, minValue))
            {
                return new ValidationResult(minValueError);
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateNullableString(object? obj, int maxLength, int minLength, string maxLengthError, string minLengthError)
        {
            if (CheckIfObjectIsNull(obj))
            {
                return ValidationResult.Success;
            }
            string text = (string)obj;
            if (CheckIfStringIsTooLong(text, maxLength))
            {
                return new ValidationResult(maxLengthError);
            }
            if (CheckIfStringIsTooShort(text, minLength))
            {
                return new ValidationResult(minLengthError);
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateNullableInt(object? obj, int maxValue, int minValue, string maxValueError, string minValueError)
        {
            if (CheckIfObjectIsNull(obj))
            {
                return ValidationResult.Success;
            }
            int value = (int)obj;
            if (CheckIfIntIsTooBig(value, maxValue))
            {
                return new ValidationResult(maxValueError);
            }
            if (CheckIfIntIsTooSmall(value, minValue))
            {
                return new ValidationResult(minValueError);
            }
            return ValidationResult.Success;
        }

        public static ValidationResult RequiredObject(object? obj, string error)
        {
            if (CheckIfObjectIsNull(obj))
            {
                return new ValidationResult(error);
            }
            return ValidationResult.Success;
        }

        public static bool CheckIfObjectIsUnique(List<object> list, object obj)
        {
            return list.Contains(obj);
        }

        private static bool CheckIfObjectIsNull(object? objectForCheck)
        {
            return objectForCheck == null;
        }

        private static bool CheckIfIntIsTooBig(int value, int maxValue)
        {
            return value > maxValue;
        }

        private static bool CheckIfIntIsTooSmall(int value, int minValue)
        {
            return value < minValue;
        }

        private static bool CheckIfStringIsTooLong(string text, int maxLength)
        {
            return text.Length > maxLength;
        }

        private static bool CheckIfStringIsTooShort(string text, int minLength)
        {
            return minLength > text.Length;
        }

    }
}
