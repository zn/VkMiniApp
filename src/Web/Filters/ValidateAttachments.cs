using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Web.ViewModels;
using Microsoft.Extensions.Options;
using Web.Config;
using System.IO;

namespace Web.Filters
{
    public class ValidateAttachments : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var viewModel = context.ActionArguments["model"] as CreatePostViewModel;
            if(viewModel == null)
            {
                context.Result = new BadRequestResult();
                return;
            }
            if (viewModel.Attachments == null)
            {
                return;
            }

            var service = context.HttpContext.RequestServices.GetRequiredService<IOptions<AttachmentsConfig>>();
            var config = service.Value;
            var files = viewModel.Attachments;

            bool isValid = files.Count() <= config.MaxNumberFiles &&
                           files.All(file => file.Length <= config.FileMaxSize &&
                                             config.AllowedExtensions.Contains(Path.GetExtension(file.FileName)));
            if (!isValid)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
