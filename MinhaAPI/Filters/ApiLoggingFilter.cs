using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        this.logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("### Executando => OnActionExecuted");
        logger.LogInformation("##################################");
        logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        logger.LogInformation("##################################");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("### Executando => OnActionExecuting");
        logger.LogInformation("##################################");
        logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
        logger.LogInformation("##################################");
    }
}
