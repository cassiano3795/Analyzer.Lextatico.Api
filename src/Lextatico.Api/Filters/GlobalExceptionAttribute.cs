using System.Threading.Tasks;
using Lextatico.Domain.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lextatico.Api.Filters
{
    /// <summary>
    /// Filter to catch exceptions during operations called by controller methods.
    /// </summary>
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Method called when an exception happens during an operation triggered by the controller.
        /// </summary>
        /// <param name="context">Context object for the exception.</param>
        public override void OnException(ExceptionContext context)
        {
            var response = new Response();

            var exception = context.Exception;

            response.AddError(string.Empty, "Ocorreu um erro inesperado.");

            var result = new ObjectResult(response);

            result.StatusCode = 500;

            context.Result = result;
        }

        /// <summary>
        /// Method called when an exception happens during an operation triggered by the controller.
        /// </summary>
        /// <param name="context">Context object for the exception.</param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);

            return Task.CompletedTask;
        }
    }
}
