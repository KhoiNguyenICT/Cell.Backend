using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    public class SettingFeatureValidator : AbstractValidator<SettingFeature>
    {
        public SettingFeatureValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PathCode).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.PathId).NotEmpty().MaximumLength(1000);
        }
    }
}