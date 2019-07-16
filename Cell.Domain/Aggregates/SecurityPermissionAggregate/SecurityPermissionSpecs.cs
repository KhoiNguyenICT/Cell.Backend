using System;
using Cell.Core.Specifications;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    public static class SecurityPermissionSpecs
    {
        public static ISpecification<SecurityPermission> GetByAuthorizedIdSpec(Guid authorizedId) =>
            new Specification<SecurityPermission>(t => t.AuthorizedId == authorizedId);

        public static ISpecification<SecurityPermission> GetByObjectIdSpec(Guid objectId) =>
            new Specification<SecurityPermission>(t => t.ObjectId == objectId);

        public static ISpecification<SecurityPermission> GetByTableTargetSpec(string tableTarget) =>
            new Specification<SecurityPermission>(t => t.AuthorizedType == tableTarget);

        public static ISpecification<SecurityPermission> GetByGroupIdSpec(Guid groupId) =>
            new Specification<SecurityPermission>(t => t.AuthorizedId == groupId);
    }
}