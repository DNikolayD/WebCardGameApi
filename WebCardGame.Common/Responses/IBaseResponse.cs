namespace WebCardGame.Common.Responses
{
    public interface IBaseResponse : IBaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public string Origin { get; set; }

        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public object Payload { get; set; }
    }
}
