using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFieldInstanceAggregate
{
    public class SettingFieldInstanceValidator : AbstractValidator<SettingFieldInstance>
    {
        public SettingFieldInstanceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Caption).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ContainerType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DataType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.StorageType).NotEmpty().MaximumLength(50);
        }
    }
}