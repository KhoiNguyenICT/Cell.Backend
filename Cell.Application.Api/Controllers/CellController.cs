using System.Collections.Generic;
using Cell.Core.Errors;
using Cell.Core.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Cell.Application.Api.Commands;
using Cell.Core.Extensions;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity, TCommand> : ControllerBase 
        where TEntity : Entity 
        where TCommand : Command
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

        protected async Task ValidateModel(Command command)
        {
            var isValidator = await EntityValidator.ValidateAsync(command.To<TEntity>());
            if (!isValidator.IsValid)
                throw new CellException(isValidator);
        }

        protected async Task ValidateModels(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                var isValidator = await EntityValidator.ValidateAsync(command.To<TEntity>());
                if (!isValidator.IsValid)
                    throw new CellException(isValidator);
            }
        }
    }
}