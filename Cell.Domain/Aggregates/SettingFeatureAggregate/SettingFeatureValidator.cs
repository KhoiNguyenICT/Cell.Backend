using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    public class SettingFeatureValidator : AbstractValidator<SettingFeature>
    {
        public SettingFeatureValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}