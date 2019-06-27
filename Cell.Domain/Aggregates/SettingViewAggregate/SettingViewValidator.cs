using FluentValidation;

namespace Cell.Domain.Aggregates.SettingViewAggregate
{
    public class SettingViewValidator : AbstractValidator<SettingView>
    {
        public SettingViewValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}