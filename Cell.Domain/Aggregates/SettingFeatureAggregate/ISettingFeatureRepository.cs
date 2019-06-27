using Cell.Core.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    public interface ISettingFeatureRepository : IRepository<SettingFeature>
    {
        Task<bool> AnyAsync();
        Task<List<SettingFeature>> GetTreeAsync();
    }
}