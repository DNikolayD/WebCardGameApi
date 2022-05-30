using FluentValidation;
using WebCardGame.Common.ValidationModels;
using WebCardGame.Common;
using WebCardGame.Common.Extensions;
using WebCardGame.Data.DataEntities.CardDataEntities;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;
using static WebCardGame.Common.ErrorHandling.StatusCodes;

namespace WebCardGame.Data.Validators
{
    internal class DeckDataEntityValidator : AbstractValidator<DeckDataEntity>
    {
        private readonly BaseValidationModel _baseValidationModel;

        public DeckDataEntityValidator()
        {
            _baseValidationModel = new BaseValidationModel(DeckClassName);
            SetRulesForName();
            SetRulesForTypeId();
            SetRulesForCreatorId();
        }

        private void SetRulesForName()
        {
            _baseValidationModel.OriginProperty = NamePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(deck => deck.Name) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
            _baseValidationModel.Value = MinNameLength;
            rulePointer.SetTooShortRule(_baseValidationModel);
            _baseValidationModel.Value = MaxNameLength;
            rulePointer.SetTooLongRule(_baseValidationModel);
        }

        private void SetRulesForTypeId()
        { 
            _baseValidationModel.OriginProperty = TypePropertyName; 
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(deck => deck.TypeId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }


        private void SetRulesForCreatorId()
        {
            _baseValidationModel.OriginProperty = CreatorPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            var rulePointer = RuleFor(deck => deck.CreatorId) as IRuleBuilder<IBaseEntity, object>;
            rulePointer.SetNotNullRule(_baseValidationModel);
        }
    }
}
