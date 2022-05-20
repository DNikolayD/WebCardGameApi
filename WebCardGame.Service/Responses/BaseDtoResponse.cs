using WebCardGame.Common.Responses;

namespace WebCardGame.Service.Responses
{
    public class BaseDtoResponse : IBaseResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public object Payload { get; set; }

        public BaseDtoResponse()
        {
            IsSuccess = Errors == null || Errors.Count == 0;
        }
    }
}
