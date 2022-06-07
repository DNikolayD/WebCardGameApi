using FluentValidation;
using WebCardGame.Common;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Data.DataEntities.CardDataEntities;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;
using static WebCardGame.Common.ErrorHandling.StatusCodes;

namespace WebCardGame.Data.Validators
{
    public class EffectTypeDataEntityValidator : AbstractValidator<EffectTypeDataEntity>
    {

        private readonly BaseValidationModel _baseValidationModel;

        public EffectTypeDataEntityValidator()
        {
            _baseValidationModel = new BaseValidationModel(EffectTypeClassName);
            SetRulesForDescription();
        }

        private void SetRulesForDescription()
        {
            _baseValidationModel.OriginProperty = DescriptionPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effectType => effectType.Description) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinDescriptionLength;
            rulePointer.SetTooShortRule(_baseValidationModel);
            _baseValidationModel.Value = MaxDescriptionLength;
            rulePointer.SetTooLongRule(_baseValidationModel);
        }
    }
}
