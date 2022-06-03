using WebCardGame.Common.Responses;

namespace WebCardGame.Common.Logger
{
    public static class MessageProvider
    {
        public static string GetMessage(BaseResponse response)
        {
            var firstPartOfMessage = $"This response was created on {response} from {response.Origin}.";
            var secondPartOfMessage = response.IsSuccess ? GetSuccessMessage(response) : GetErrorMessage(response);
            var message = string.Concat(firstPartOfMessage, secondPartOfMessage);
            return message;
        }

        private static string GetSuccessMessage(IBaseResponse response)
        {
            return
                $" The response contains no errors and it has payload of type {response.Payload.GetType().Name}";
        }

        private static string GetErrorMessage(IBaseResponse response)
        {
            return
                $" The response contains {response.Errors.Count} errors: {response.Errors.Select(e => e + "\n")}";
        }
    }
}
