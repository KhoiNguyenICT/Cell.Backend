using FluentValidation;

namespace Cell.Domain.Aggregates.SettingTableAggregate
{
    public class SettingTableValidator : AbstractValidator<SettingTable>
    {
        public SettingTableValidator()
        {
            RuleFor(x => x.BasedTable).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}