using FluentValidation;

namespace Cell.Domain.Aggregates.SettingReportAggregate
{
    public class SettingReportValidator : AbstractValidator<SettingReport>
    {
        public SettingReportValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}