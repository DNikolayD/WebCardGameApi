using System.ComponentModel.DataAnnotations;
using WebCardGame.Common.Checkers;
using WebCardGame.Common.CustomValidationAttributes;
using WebCardGame.Data.DataEntities.Base;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.EffectDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public string Id { get; set; } 

        [UniversalValidation(EffectClassName, NamePropertyName, CheckType.All, true, MaxNameLength, MinNameLength)]
        public string Name { get; set; }

        [UniversalValidation(EffectClassName, DescriptionPropertyName, CheckType.All, true, MaxDescriptionLength, MinDescriptionLength)]
        public string Description { get; set; }

        [UniversalValidation(EffectClassName, TypePropertyName, CheckType.Empty, false)]
        public int TypeId { get; set; }

        public virtual EffectTypeDataEntity Type { get; set; }

        [UniversalValidation(EffectClassName, ValuePropertyName, CheckType.MaxAndMinValue, false, MaxValue, MinValue)]
        public int? Value { get; set; }

        [UniversalValidation(EffectClassName, DurationPropertyName, CheckType.MaxAndMinValue, false, MaxDuration, MinDuration)]
        public int? Duration { get; set; }

        public string? Target { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public EffectDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
