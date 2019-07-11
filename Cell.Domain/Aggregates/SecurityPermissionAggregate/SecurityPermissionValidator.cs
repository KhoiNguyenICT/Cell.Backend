using FluentValidation;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    public class SecurityPermissionValidator : AbstractValidator<SecurityPermission>
    {
        public SecurityPermissionValidator()
        {
            RuleFor(x => x.AuthorizedId).NotEmpty();
            RuleFor(x => x.AuthorizedType).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ObjectId).NotEmpty();
            RuleFor(x => x.ObjectName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.TableName).NotEmpty().MaximumLength(200);
        }
    }
}