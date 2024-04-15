using InventoryMgmt.Validation_Rules;
using InventoryMgmt.Validations;
using Microsoft.AspNetCore.Mvc.Filters;
namespace InventoryMgmt
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
            new AddItemValidationRules();
            new StoreValidationRules();
            //new UserValidationRules();
            new StockValidationRules();

        }

        //public void OnResultExecuted(ResultExecutedContext context)
        //{
        //    //throw new NotImplementedException();
        //}

        //public void OnResultExecuting(ResultExecutingContext context)
        //{
        //    //throw new NotImplementedException();
        //    ItemValidationRules itemValidationRule = new ItemValidationRules();
        //    StoreValidationRules storeValidationRule = new StoreValidationRules();
        //    UserValidationRules userValidationRule = new UserValidationRules();
        //    StockValidationRules stockValidationRule = new StockValidationRules();
        //}
    }
}
