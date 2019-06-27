using FluentValidation;

namespace Cell.Domain.Aggregates.SettingAdvancedAggregate
{
    public class SettingAdvancedValidator : AbstractValidator<SettingAdvanced>
    {
        public SettingAdvancedValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}