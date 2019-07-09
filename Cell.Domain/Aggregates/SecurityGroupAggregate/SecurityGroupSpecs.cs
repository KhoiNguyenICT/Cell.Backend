using System;
using Cell.Core.Specifications;

namespace Cell.Domain.Aggregates.SecurityGroupAggregate
{
    public class SecurityGroupSpecs
    {
        public static ISpecification<SecurityGroup> GetByCodeSpec(string code) =>
            new Specification<SecurityGroup>(t => t.Code == code);
    }
}