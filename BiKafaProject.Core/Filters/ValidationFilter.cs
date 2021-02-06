using BiKafaProject.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BiKafaProject.Core.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.status = 400;
                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(ms => ms.Errors);
                modelErrors.ToList().ForEach(me =>
                {
                    errorDto.Errors.Add(me.ErrorMessage);
                });
                context.Result = new BadRequestObjectResult(errorDto);
            }
        }
    }
}
