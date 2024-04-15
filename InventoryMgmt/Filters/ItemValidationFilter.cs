using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryMgmt.Filters
{
    public class ItemValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Filters after execution");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Filters before execution");

        }
    }
}
