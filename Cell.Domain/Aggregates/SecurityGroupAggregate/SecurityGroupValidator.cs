using FluentValidation;

namespace Cell.Domain.Aggregates.SecurityGroupAggregate
{
    public class SecurityGroupValidator : AbstractValidator<SecurityGroup>
    {
        public SecurityGroupValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}