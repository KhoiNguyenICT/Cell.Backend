using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Model.Models.SecurityUser
{
    public static class SecurityUserSpecs
    {
        public static ISpecification<Model.Entities.SecurityUserEntity.SecurityUser> SearchByQuery(string query) =>
            new Specification<Model.Entities.SecurityUserEntity.SecurityUser>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Account, $"%{query}%") ||
                EF.Functions.Like(t.Email, $"%{query}%"));

        public static ISpecification<Model.Entities.SecurityUserEntity.SecurityUser> GetByAccountSpec(string account) =>
            new Specification<Model.Entities.SecurityUserEntity.SecurityUser>(t => t.Account == account);

        public static ISpecification<Model.Entities.SecurityUserEntity.SecurityUser> GetByEmailSpec(string email) =>
            new Specification<Model.Entities.SecurityUserEntity.SecurityUser>(t => t.Email == email);
    }
}