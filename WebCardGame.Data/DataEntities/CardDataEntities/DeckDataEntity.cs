using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class DeckDataEntity : DataEntityWithStringKey
    {
        public string Name { get; set; }

        public int TypeId { get; set; }

        public virtual DeckTypeDataEntity Type { get; set; }

        public string CreatorId { get; set; }

        public virtual UserDataEntity Creator { get; set; }

        public virtual List<UserDataEntity> Users { get; set; }

        public virtual List<CardDataEntity> Cards { get; set; }

        public DeckDataEntity()
        {
            Type = new DeckTypeDataEntity();
            Creator = new UserDataEntity();
            Users = new List<UserDataEntity>();
            Cards = new List<CardDataEntity>();
        }
    }
}
