using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;


namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }

        public virtual EffectTypeDataEntity Type { get; set; }

        public int? Value { get; set; }

        public int? Duration { get; set; }

        public string? Target { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public EffectDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
