using Cell.Core.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController : ControllerBase
    {
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var formattedError = FormatError(modelState);
            return new BadRequestObjectResult(formattedError);
        }

        private CellError FormatError(ModelStateDictionary modelStateDictionary)
        {
            return new CellError(
                modelStateDictionary
                    .Where(t => !string.IsNullOrEmpty(t.Key) && t.Value.ValidationState == ModelValidationState.Invalid)
                    .SelectMany(t => t.Value.Errors.Select(x => new CellValidationError(t.Key.IndexOf('.') > 0 ? t.Key.Substring(t.Key.IndexOf('.') + 1) : t.Key, x.ErrorMessage)))
            );
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        protected virtual void ValidateModelState(ActionExecutingContext context)
        {
            if (ModelState.IsValid) return;
            context.Result = BadRequest(ModelState);
        }
    }
}