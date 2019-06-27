using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFormAggregate
{
    public class SettingFormValidator : AbstractValidator<SettingForm>
    {
        public SettingFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}