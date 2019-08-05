using System;
using Cell.Common.Specifications;

namespace Cell.Model.Models.SecuritySession
{
    public static class SecuritySessionSpecs
    {
        public static ISpecification<Entities.SecuritySessionEntity.SecuritySession> GetBySessionIdSpec(Guid sessionId) =>
            new Specification<Entities.SecuritySessionEntity.SecuritySession>(t => t.Id == sessionId);
    }
}