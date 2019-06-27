using Cell.Core.Errors;
using Cell.Core.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using FluentValidation;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity> : ControllerBase where TEntity : Entity
    {
        protected readonly IValidator<TEntity> EntityValidator;

        public CellController(IValidator<TEntity> entityValidator)
        {
            EntityValidator = entityValidator;
        }

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
    }
}