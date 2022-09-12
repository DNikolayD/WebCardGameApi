using WebCardGame.Data.DataEntities.CardDataEntities;

namespace WebCardGame.Data.DataEntities.Base
{
    public class ImageDataEntity : DataEntityWithIntKey
    {
        public string ImagePath { get; set; }

        public virtual CardDataEntity CardDataEntity { get; set; }

    }
}