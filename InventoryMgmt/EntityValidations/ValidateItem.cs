using FluentValidation;
using FluentValidation.Results;
using InventoryMgmt.Model;
using InventoryMgmt.Validations;

namespace InventoryMgmt.EntityValidations
{
    public class ValidateItem
    {
        ItemModel itemModel = new ItemModel();
        ItemValidationRules validations = new ItemValidationRules();
        public ValidateItem()
        {
            
            validations.ValidateAndThrow(itemModel);
            //ValidationResult result = validations.Validate(itemModel);
            //if (!result.IsValid)
            //{
            //    foreach (var item in result.Errors)
            //    {
            //        throw new Exception($"Property {item.PropertyName} failed validation. Error was: {item.ErrorMessage}");
            //    }
            //}
        }

    }
}
