using BiKafaProject.Core.DTOs;
using BiKafaProject.Core.Interfaces;
using BiKafaProject.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiKafaProject.Core.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly IUserOperationsRepository _userModel;

        public NotFoundFilter(IUserOperationsRepository userModel)
        {
            _userModel = userModel;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IEnumerable<UserModel> user = null;
            string id = null;

            if (context.ActionArguments.Values.Count() != 0)
            {
                id = (string)context.ActionArguments.Values.FirstOrDefault();

                user = await _userModel.GetAsync(id);
            }


            if (id == null  )
            {
                await next();
            }
            if (id != null && user.Count() > 0 )
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.status = 404;

                errorDto.Errors.Add($"id'si {id} olan data veri tabanında bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);
            }

        }
    }
}
