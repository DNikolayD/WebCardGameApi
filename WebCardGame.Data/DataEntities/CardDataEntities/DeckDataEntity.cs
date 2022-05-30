using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class DeckDataEntity : DataEntityWithStringKey
    {

        public string Name { get; set; }

        public int TypeId { get; set; }

        public DeckTypeDataEntity Type { get; set; }

        public string CreatorId { get; set; }

        public UserDataEntity Creator { get; set; }

        public List<UserDataEntity> Users { get; set; }

        public List<CardDataEntity> Cards { get; set; }

    }
}
