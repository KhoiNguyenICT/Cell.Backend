using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SecurityUserAggregate
{
    public class SecurityUserSpecs
    {
        public static ISpecification<SecurityUser> SearchByQuery(string query) =>
            new Specification<SecurityUser>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Account, $"%{query}%") ||
                EF.Functions.Like(t.Email, $"%{query}%"));

        public static ISpecification<SecurityUser> GetByAccountSpec(string account) =>
            new Specification<SecurityUser>(t => t.Account == account);

        public static ISpecification<SecurityUser> GetByEmailSpec(string email) =>
            new Specification<SecurityUser>(t => t.Email == email);
    }
}