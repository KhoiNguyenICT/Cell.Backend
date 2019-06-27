using FluentValidation;

namespace Cell.Domain.Aggregates.SettingActionAggregate
{
    public class SettingActionValidator : AbstractValidator<SettingAction>
    {
        public SettingActionValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ContainerType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}