using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public sealed class CardTypeDataEntity : DataEntityWithIntKey
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CardDataEntity> CardDataEntities { get; set; }

        public CardTypeDataEntity()
        {
            CardDataEntities = new List<CardDataEntity>();
        }
    }
}
