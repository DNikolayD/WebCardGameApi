using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class CardDataEntity : IDeletableDataEntity<object>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public virtual ImageDataEntity ImageDataEntity { get; set; }

        public int TypeId { get; set; }

        public virtual CardTypeDataEntity Type { get; set; }

        public virtual List<EffectDataEntity> Effects { get; set; }

        public int Cost { get; set; }

        public string CreatorId { get; set; }

        public UserDataEntity Creator { get; set; }

        public string? DeckId { get; set; }

        public virtual DeckDataEntity? Deck { get; set; }
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }

        object IDeletableDataEntity<object>.Id { get; set; }

        public CardDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
