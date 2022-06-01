using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCardGame.Common.Responses;

namespace WebCardGame.Common.Logger
{
    public static class BaseMessageProvider
    {
        public static string GetMessage(BaseResponse response)
        {
            if (response.IsSuccess)
            {
                return GetSuccessMessage(response);
            }

            return GetErrorMessage(response);
        }

        private static string GetSuccessMessage(BaseResponse response)
        {
            return
                $"This response was created on {response} from {response.Origin}. The response contains no errors and it has payload of type {response.Payload.GetType().Name}";
        }

        private static string GetErrorMessage(BaseResponse response)
        {
            return
                $"This response was created on {response} from {response.Origin}. The response contains {response.Errors.Count} errors: {response.Errors.Select(e => e + "\n")}";
        }
    }
}
