using System.ComponentModel.DataAnnotations;
using WebCardGame.Common.Checkers;
using WebCardGame.Common.CustomValidationAttributes;
using WebCardGame.Data.DataEntities.Base;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class EffectTypeDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public int Id { get; set; }

        [UniversalValidation(EffectTypeClassName, NamePropertyName, CheckType.All, true, MaxNameLength, MinNameLength)]
        public string Name { get; set; }

        [UniversalValidation(EffectTypeClassName, DescriptionPropertyName, CheckType.All, true, MaxDescriptionLength, MinDescriptionLength)]
        public string Description { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public EffectTypeDataEntity()
        {
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
