using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class CardTypeDataEntity : DataEntityWithIntKey
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CardDataEntity> CardDataEntities { get; set; }

    }
}
