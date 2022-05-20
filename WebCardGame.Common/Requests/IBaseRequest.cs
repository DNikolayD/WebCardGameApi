namespace WebCardGame.Common.Requests
{
    public interface IBaseRequest : IBaseEntity
    {
        public string Origin { get; set; }

        public string Type { get; set; }

        public object Payload { get; set; }
    }
}
