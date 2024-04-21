using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InventoryMgmt
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;
        public ExceptionHandling(
                RequestDelegate next,
                ILogger<ExceptionHandling> logger
            )
        {
            _next= next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            //how to see this log
            // what type of error can occur in HttpContext
            catch(Exception ex)
            {
                Log.Error(ex, $"Exception Occured {ex.Message}");
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Type = "HttpContext Error",
                    Detail= ex.Message,

                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
