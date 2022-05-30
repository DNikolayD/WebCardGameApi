using FluentValidation;
using WebCardGame.Common;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Data.DataEntities.CardDataEntities;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.EffectDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;
using static WebCardGame.Common.ErrorHandling.StatusCodes;

namespace WebCardGame.Data.Validators
{
    public class EffectDataEntityValidator : AbstractValidator<EffectDataEntity>
    {
        private readonly BaseValidationModel _baseValidationModel;

        public EffectDataEntityValidator()
        {
            _baseValidationModel = new BaseValidationModel(EffectClassName);
            SetRulesForName();
            SetRulesForDescription();
            SetRulesForTypeId();
            SetRulesForValue();
            SetRulesForDuration();
        }

        private void SetRulesForName()
        {
            _baseValidationModel.OriginProperty = NamePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effect => effect.Name) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinNameLength;
            rulePointer.SetTooShortRule(_baseValidationModel);
            _baseValidationModel.Value = MaxNameLength;
            rulePointer.SetTooLongRule(_baseValidationModel);
        }

        private void SetRulesForDescription()
        {
            _baseValidationModel.OriginProperty = DescriptionPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effect => effect.Description) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinDescriptionLength;
            rulePointer.SetTooShortRule(_baseValidationModel);
            _baseValidationModel.Value = MaxDescriptionLength;
            rulePointer.SetTooLongRule(_baseValidationModel);
        }

        private void SetRulesForTypeId()
        {
            _baseValidationModel.OriginProperty = TypePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effect => effect.TypeId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }

        private void SetRulesForValue()
        {
            _baseValidationModel.OriginProperty = CreatorPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effect => effect.Value) as IRuleBuilder<IBaseEntity, object>;
            _baseValidationModel.Value = MinValue;
            rulePointer.SetTooSmallRule(_baseValidationModel);
            _baseValidationModel.Value = MaxValue;
            rulePointer.SetTooBigRule(_baseValidationModel);
        }

        private void SetRulesForDuration()
        {
            _baseValidationModel.OriginProperty = CreatorPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(effect => effect.Duration) as IRuleBuilder<IBaseEntity, object>;
            _baseValidationModel.Value = MinDuration;
            rulePointer.SetTooSmallRule(_baseValidationModel);
            _baseValidationModel.Value = MaxDuration;
            rulePointer.SetTooBigRule(_baseValidationModel);
        }
    }
}
