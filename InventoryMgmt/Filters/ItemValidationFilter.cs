using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryMgmt.Filters
{

    //This is just the demo class to understand that the Filters are called before and after each api endpoint call

    // this is referenced in the program.cs builder.Services.AddControllers
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
