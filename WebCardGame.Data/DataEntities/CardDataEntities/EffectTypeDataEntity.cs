using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectTypeDataEntity : DataEntityWithIntKey
    {

        public string Description { get; set; }

        public EffectTypeDataEntity()
        {
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
