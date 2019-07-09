using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using FluentValidation;

namespace Cell.Application.Api.Controllers
{
    public class SettingSessionController : CellController<SecuritySession, SettingSessionCommand>
    {
        public SettingSessionController(IValidator<SecuritySession> entityValidator) : base(entityValidator)
        {
        }
    }
}