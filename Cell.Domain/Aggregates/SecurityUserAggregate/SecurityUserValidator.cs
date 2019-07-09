using FluentValidation;

namespace Cell.Domain.Aggregates.SecurityUserAggregate
{
    public class SecurityUserValidator : AbstractValidator<SecurityUser>
    {
        public SecurityUserValidator()
        {
            RuleFor(x => x.Account).MaximumLength(200).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(200).NotEmpty();
            RuleFor(x => x.Phone).MaximumLength(50).NotEmpty();
        }
    }
}