using WebCardGame.Common.Responses;

namespace WebCardGame.Data.Responses
{
    public class BaseDataResponse : IBaseResponse
    {

        public DateTime CreatedOn { get; set; }

        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public object Payload { get; set; }

        public BaseDataResponse()
        {
            CreatedOn = DateTime.UtcNow;
            this.IsSuccess = Errors == null || Errors.Count == 0;
        }
    }
}
