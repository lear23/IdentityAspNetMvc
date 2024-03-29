using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]

public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = config.GetValue<string>("ApiKey");


        if (!context.HttpContext.Request.Query.TryGetValue("Key", out var provkey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!apiKey!.Equals(provkey))
        {
            context.Result = new UnauthorizedResult();
            return;
        } 

        await next();
    }
}
