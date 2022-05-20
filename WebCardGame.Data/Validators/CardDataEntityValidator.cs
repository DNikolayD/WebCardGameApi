using FluentValidation;
using WebCardGame.Common;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Data.DataEntities.CardDataEntities;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.CardDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;
using static WebCardGame.Common.ErrorHandling.StatusCodes;

namespace WebCardGame.Data.Validators
{
    public class CardDataEntityValidator : AbstractValidator<CardDataEntity>
    {

        private readonly BaseValidationModel _baseValidationModel;

        public CardDataEntityValidator()
        {
            _baseValidationModel = new BaseValidationModel(CardClassName);
            SetRulesForName();
            SetRulesForDescription();
            SetRulesForImageId();
            SetRulesForTypeId();
            SetRulesForEffects();
            SetRulesForCost();
            SetRulesForCreatorId();
        }

        private void SetRulesForName()
        {
            _baseValidationModel.OriginProperty = NamePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.Name) as IRuleBuilder<IBaseEntity, object>;
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
            var rulePointer = RuleFor(card => card.Description) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinDescriptionLength;
            rulePointer.SetTooShortRule(_baseValidationModel);
            _baseValidationModel.Value = MaxDescriptionLength;
            rulePointer.SetTooLongRule(_baseValidationModel);
        }

        private void SetRulesForImageId()
        {
            _baseValidationModel.OriginProperty = ImagePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.ImageId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }

        private void SetRulesForTypeId()
        {
            _baseValidationModel.OriginProperty = TypePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.TypeId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }

        private void SetRulesForEffects()
        {
            _baseValidationModel.OriginProperty = EffectPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.Effects) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }

        private void SetRulesForCost()
        {
            _baseValidationModel.OriginProperty = CostPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.Cost) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinCostValue;
            rulePointer.SetTooSmallRule(_baseValidationModel);
            _baseValidationModel.Value = MaxCostValue;
            rulePointer.SetTooBigRule(_baseValidationModel);
        }

        private void SetRulesForCreatorId()
        {
            _baseValidationModel.OriginProperty = CreatorPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(card => card.CreatorId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }
    }
}
