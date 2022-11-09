using WebCardGame.Common.ErrorHandling;
using WebCardGame.Common.Requests;
using WebCardGame.Common.Responses;
using WebCardGame.Common.ValidationModels;
using static WebCardGame.Common.AllowedOrigins.CardService.AllowedOriginsForCardServices;
using static WebCardGame.Common.AllowedTypes.CardService.AllowedTypesForCardServices;

namespace WebCardGame.Common.Builders
{
    public static class ResponseBuilder
    {
        public static BaseResponse BuildBaseResponse(string className, string propertyName, BaseRequest request)
        {
            var calledFrom = request.Origin;
            var type = request.Type;
            var origin = OriginBuilder.Origin(className, propertyName);
            var baseDtoResponse = new BaseResponse()
            {
                Origin = origin
            };
            if (!AllowedOriginsForAddAsync.Contains(calledFrom))
            {
                var originError = new BaseValidationModel(className)
                {
                    CalledFrom = calledFrom,
                    TypeOfCall = type,
                    ErrorCode = "403 Not Allowed",
                    ErrorType = ErrorType.WrongOrigin,
                    OriginProperty = propertyName,
                    Value = 0
                }.ConstructErrorMessage();
                baseDtoResponse.Errors.Add(originError);
            }

            if (!AllowedTypesForAddAsync.Contains(type))
            {
                var typeError = new BaseValidationModel(className)
                {
                    CalledFrom = calledFrom,
                    TypeOfCall = type,
                    ErrorCode = "403 Not Allowed",
                    ErrorType = ErrorType.WrongType,
                    OriginProperty = propertyName,
                    Value = 0
                }.ConstructErrorMessage();
                baseDtoResponse.Errors.Add(typeError);
            }

            return baseDtoResponse;
        }
    }
}
