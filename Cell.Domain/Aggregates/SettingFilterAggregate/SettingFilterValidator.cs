using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFilterAggregate
{
    public class SettingFilterValidator : AbstractValidator<SettingFilter>
    {
        public SettingFilterValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}