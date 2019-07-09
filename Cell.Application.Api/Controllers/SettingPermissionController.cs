using Cell.Application.Api.Commands;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cell.Application.Api.Controllers
{
    public class SettingPermissionController : CellController<SecurityPermission, SettingPermissionCommand>
    {
        public SettingPermissionController(IValidator<SecurityPermission> entityValidator) : base(entityValidator)
        {
        }
    }
}