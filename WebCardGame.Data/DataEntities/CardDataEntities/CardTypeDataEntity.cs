using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class CardTypeDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        object IDeletableDataEntity<object>.Id { get; set; }

        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public CardTypeDataEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
        }
    }
}
