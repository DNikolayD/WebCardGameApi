using WebCardGame.Common.Requests;

namespace WebCardGame.Service.Requests
{
    public class BaseDtoRequest : IBaseRequest
    {
        public string Origin { get; set; }
        public string Type { get; set; }
        public object Payload { get; set; }
    }
}
