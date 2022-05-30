using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class CardDataEntity : DataEntityWithStringKey
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
    }
}
