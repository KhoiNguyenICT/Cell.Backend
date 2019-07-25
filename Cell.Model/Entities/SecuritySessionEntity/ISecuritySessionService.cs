using System;
using System.Threading.Tasks;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SecuritySessionEntity
{
    public interface ISecuritySessionService : IService<SecuritySession>
    {
        Task<bool> VerifySession(Guid id);
    }
}