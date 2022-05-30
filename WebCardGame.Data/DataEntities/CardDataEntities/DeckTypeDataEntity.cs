using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class DeckTypeDataEntity : DataEntityWithIntKey
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}