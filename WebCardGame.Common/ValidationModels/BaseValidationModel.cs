using WebCardGame.Common.ErrorHandling;

namespace WebCardGame.Common.ValidationModels
{
    public class BaseValidationModel
    {
        public ErrorType ErrorType { get; set; }

        public string OriginClass { get; set; }

        public string OriginProperty { get; set; }

        public int Value { get; set; }

        public string ErrorMessage { get; }

        public string ErrorCode { get; set; }

        public BaseValidationModel(string originClass)
        {
            OriginProperty = originClass;
            ErrorMessage = this.ConstructErrorMessage();
        }
    }
}
