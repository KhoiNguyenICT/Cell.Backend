using System;
using Cell.Core.Specifications;

namespace Cell.Domain.Aggregates.SecuritySessionAggregate
{
    public static class SecuritySessionSpecs
    {
        public static ISpecification<SecuritySession> GetBySessionIdSpec(Guid sessionId) =>
            new Specification<SecuritySession>(t => t.Id == sessionId);
    }
}