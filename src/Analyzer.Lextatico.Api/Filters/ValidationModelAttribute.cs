using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analyzer.Lextatico.Application.Dtos.Response;
using Analyzer.Lextatico.Domain.Dtos.Message;
using Analyzer.Lextatico.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Analyzer.Lextatico.Api.Filters
{
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new Response();

                foreach (var key in context.ModelState.Keys)
                {
                    var value = context.ModelState[key];
                    foreach (var error in value.Errors)
                    {
                        response.AddError(key.ToCamelCase(), error.ErrorMessage);
                    }
                }

                context.Result = new UnprocessableEntityObjectResult(response);
            }
        }
    }
}
