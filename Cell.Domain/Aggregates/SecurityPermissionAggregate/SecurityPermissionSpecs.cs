using System;
using Cell.Core.Specifications;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    public class SecurityPermissionSpecs
    {
        public static ISpecification<SecurityPermission> GetByAuthorizedIdSpec(Guid authorizedId) =>
            new Specification<SecurityPermission>(t => t.AuthorizedId == authorizedId);

        public static ISpecification<SecurityPermission> GetByObjectIdSpec(Guid objectId) =>
            new Specification<SecurityPermission>(t => t.ObjectId == objectId);
    }
}