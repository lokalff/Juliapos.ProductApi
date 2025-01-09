using Juliapos.Portal.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Juliapos.Portal.ProductApi.Api
{
    /// <summary>
    /// Generic way to handle ApiResultException
    /// </summary>
    public sealed class ApiResultExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiResultException ex)
            { 
                context.Result = new ObjectResult(ErrorResultFactory.CreateErrorResult(ex)) { StatusCode = (int)ex.StatusCode };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is HttpForbidException)
            {
                context.Result = new ForbidResult();
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new BadRequestObjectResult(context.Exception);
                context.ExceptionHandled = true;
            }
        }
    }
}
