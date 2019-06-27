using System.Data;
using FluentValidation;

namespace Cell.Domain.Aggregates.SettingFieldAggregate
{
    public class SettingFieldValidator : AbstractValidator<SettingField>
    {
        public SettingFieldValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Caption).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DataType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PlaceHolder).NotEmpty().MaximumLength(200);
            RuleFor(x => x.StorageType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}