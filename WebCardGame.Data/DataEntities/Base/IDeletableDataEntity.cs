namespace WebCardGame.Data.DataEntities.Base
{
    public interface IDeletableDataEntity<TKey> : IDataEntity
    {
        public TKey Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }
    }
}
