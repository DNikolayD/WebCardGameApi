using System.ComponentModel.DataAnnotations;
using WebCardGame.Common.Checkers;
using WebCardGame.Common.CustomValidationAttributes;
using WebCardGame.Data.DataEntities.Base;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.CardDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class CardDataEntity : IDeletableDataEntity<object>
    {
        [UniversalValidation(CardClassName, NamePropertyName, CheckType.All, true, MaxNameLength, MinNameLength)]
        public string Name { get; set; }

        [UniversalValidation(CardClassName, DescriptionPropertyName, CheckType.All, true, MaxDescriptionLength, MinDescriptionLength)]
        public string Descrption { get; set; }

        [UniversalValidation(CardClassName, ImagePropertyName, CheckType.Empty, false)]
        public string ImageId { get; set; }

        public virtual ImageDataEntity ImageDataEntity { get; set; }

        [UniversalValidation(CardClassName, TypePropertyName, CheckType.Empty, false)]
        public int TypeId { get; set; }

        public virtual CardTypeDataEntity Type { get; set; }

        [UniversalValidation(CardClassName, EffectPropertyName, CheckType.Empty, false)]
        public virtual List<EffectDataEntity> Effects { get; set; }

        [UniversalValidation(CardClassName, CostPropertyName, CheckType.All, false, MaxCostValue, MinCostValue)]
        public int Cost { get; set; }

        [UniversalValidation(CardClassName, CreatorPropertyName, CheckType.Empty, false)]
        public string CreatorId { get; set; }

        public UserDataEntity Creator { get; set; }

        public string? DeckId { get; set; }

        public virtual DeckDataEntity? Deck { get; set; }
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }

        object IDeletableDataEntity<object>.Id { get; set; }

        public CardDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
