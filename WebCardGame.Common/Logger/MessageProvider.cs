using WebCardGame.Common.Responses;

namespace WebCardGame.Common.Logger
{
    public static class MessageProvider
    {
        public static string GetMessage(this IBaseResponse response)
        {
            var firstPartOfMessage = $"This response was created on {response.CreatedOn.Day} from {response.Origin}.";
            var secondPartOfMessage = response.IsSuccess ? response.GetSuccessMessage() : response.GetErrorMessage();
            var message = string.Concat(firstPartOfMessage, secondPartOfMessage);
            return message;
        }

        private static string GetSuccessMessage(this IBaseResponse response)
        {
            return
                $" The response contains no errors and it has payload of type {response.Payload.GetType().Name}";
        }

        private static string GetErrorMessage(this IBaseResponse response)
        {
            return
                $" The response contains {response.Errors.Count} errors: {response.Errors.Select(e => e + "\n")}";
        }
    }
}
