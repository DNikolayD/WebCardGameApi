using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCardGame.Common.Extensions;
using WebCardGame.Common.Requests;
using WebCardGame.Common.ValidationModels;
using static WebCardGame.Common.Checkers.ClassNames;
using static WebCardGame.Common.Checkers.PropertyNames;
using static WebCardGame.Common.DataConstraints.CardDataConstraints;
using static WebCardGame.Common.DataConstraints.StandardDataConstraints;
using static WebCardGame.Common.ErrorHandling.StatusCodes;

namespace WebCardGame.Common
{
    public class BaseValidator
    {
        private readonly BaseValidationModel _baseValidationModel;
        private object _entity;

        public BaseValidator(BaseValidationModel baseValidationModel)
        {
            _baseValidationModel = baseValidationModel;
            Errors = new HashSet<string>();
        }

        public HashSet<string> Errors { get; set; }

        public void Validate(object entity)
        {
            _entity = entity;
            SetRulesForName();
            SetRulesForCost();
            SetRulesForDescription();
            SetRulesForCreatorId();
            SetRulesForEffects();
            SetRulesForImageId();
            SetRulesForTypeId();
            if (Errors.Any(x => !x.BeNotNull()))
            {
                Errors.Remove(null!);
            }
        }

        private void SetRulesForName()
        {
            var property = _entity.GetType().GetProperty("Name").GetValue(_entity);
            _baseValidationModel.OriginProperty = NamePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MinNameLength;
            _baseValidationModel.SetTooShortRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MaxNameLength;
            _baseValidationModel.SetTooLongRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForDescription()
        {
            var property = _entity.GetType().GetProperty("Description").GetValue(_entity);
            _baseValidationModel.OriginProperty = DescriptionPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MinDescriptionLength;
            _baseValidationModel.SetTooShortRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MaxDescriptionLength;
            _baseValidationModel.SetTooLongRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForImageId()
        {
            var property = _entity.GetType().GetProperty("ImageId").GetValue(_entity);
            _baseValidationModel.OriginProperty = ImagePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForTypeId()
        {
            var property = _entity.GetType().GetProperty("TypeId").GetValue(_entity);
            _baseValidationModel.OriginProperty = TypePropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForEffects()
        {
            var property = _entity.GetType().GetProperty("EffectIds").GetValue(_entity);
            _baseValidationModel.OriginProperty = EffectPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForCost()
        {
            var property = _entity.GetType().GetProperty("Cost").GetValue(_entity);
            _baseValidationModel.OriginProperty = CostPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MinCostValue;
            _baseValidationModel.SetTooSmallRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
            _baseValidationModel.Value = MaxCostValue;
            _baseValidationModel.SetTooBigRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }

        private void SetRulesForCreatorId()
        {            
            var property = _entity.GetType().GetProperty("CreatorId").GetValue(_entity);
            _baseValidationModel.OriginProperty = CreatorPropertyName;
            _baseValidationModel.ErrorCode = BadRequest;
            _baseValidationModel.SetNotNullRule(property);
            Errors.Add(_baseValidationModel.ErrorMessage);
        }
    }
}
