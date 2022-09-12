namespace WebCardGame.Common.Responses
{
    public class BaseResponse : IBaseResponse
    {
        public DateTime CreatedOn { get; set; }

        public string Origin { get; set; }

        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public object Payload { get; set; }

        public BaseResponse()
        {
            CreatedOn = DateTime.UtcNow;
            Errors = new List<string>();
            IsSuccessful = !Errors.Any();
        }
    }
}
