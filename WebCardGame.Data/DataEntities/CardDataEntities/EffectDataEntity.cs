using WebCardGame.Data.DataEntities.Base;


namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectDataEntity : DataEntityWithStringKey
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }

        public virtual EffectTypeDataEntity Type { get; set; }

        public int? Value { get; set; }

        public int? Duration { get; set; }

        public string? Target { get; set; }

        public virtual ICollection<CardDataEntity> CardDataEntities { get; set; }

        public EffectDataEntity()
        {
            Type = new EffectTypeDataEntity();
            CardDataEntities = new List<CardDataEntity>();
        }
    }
}
