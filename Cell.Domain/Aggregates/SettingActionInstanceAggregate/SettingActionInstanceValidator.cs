using FluentValidation;

namespace Cell.Domain.Aggregates.SettingActionInstanceAggregate
{
    public class SettingActionInstanceValidator : AbstractValidator<SettingActionInstance>
    {
        public SettingActionInstanceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ContainerType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}