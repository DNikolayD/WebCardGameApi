using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectTypeDataEntity : DataEntityWithIntKey
    {
        public string Description { get; set; }

        public ICollection<EffectDataEntity> Effects { get; set; }

        public EffectTypeDataEntity()
        {
            Effects = new List<EffectDataEntity>();
        }
    }
}
