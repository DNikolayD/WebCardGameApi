namespace WebCardGame.Common.Requests
{
    public class BaseRequest : IBaseRequest
    {
        public string Origin { get; set; }
        public string Type { get; set; }
        public object Payload { get; set; }
    }
}
