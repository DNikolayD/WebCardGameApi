using System.ComponentModel.DataAnnotations;
using WebCardGame.Common.Checkers;
using WebCardGame.Common.CustomValidationAttributes;
using WebCardGame.Data.DataEntities.Base;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class DeckTypeDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public int Id { get; set; }

        [UniversalValidation(DeckTypeClassName, NamePropertyName, CheckType.All, true, MaxNameLength, MinNameLength)]
        public string Name { get; set; }

        [UniversalValidation(DeckTypeClassName, DescriptionPropertyName, CheckType.All, true, MaxDescriptionLength, MinDescriptionLength)]
        public string Description { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public DeckTypeDataEntity()
        {
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }

    }
}