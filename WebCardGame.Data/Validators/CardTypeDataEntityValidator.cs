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
    public class CardTypeDataEntityValidator : AbstractValidator<CardTypeDataEntity>
    {
        private readonly BaseValidationModel _baseValidationModel;

        public CardTypeDataEntityValidator()
        {
            _baseValidationModel = new BaseValidationModel(CardClassName);
            SetRulesForName();
            SetRulesForDescription();
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
    }
}
