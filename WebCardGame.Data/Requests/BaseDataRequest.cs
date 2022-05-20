using WebCardGame.Common.Requests;

namespace WebCardGame.Data.Requests
{
    public class BaseDataRequest : IBaseRequest
    {
        public string Origin { get; set; }
        public string Type { get; set; }
        public object Payload { get; set; }
    }
}
