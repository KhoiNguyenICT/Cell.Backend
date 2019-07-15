using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecuritySessionAggregate
{
    public interface ISecuritySessionRepository : IRepository<SecuritySession>
    {
        Task<bool> VerifySession(Guid id);
    }
}