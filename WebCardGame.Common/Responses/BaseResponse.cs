namespace WebCardGame.Common.Responses
{
    public class BaseResponse : IBaseResponse
    {
        public DateTime CreatedOn { get; set; }

        public string Origin { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }

        public object Payload { get; set; }

        protected BaseResponse()
        {
            CreatedOn = DateTime.UtcNow;
            Errors = new List<string>();
            IsSuccess = !Errors.Any();
        }
    }
}
