using System;
using Cell.Core.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    public interface ISettingFeatureRepository : IRepository<SettingFeature>
    {
        Task<bool> AnyAsync();
        Task<List<SettingFeature>> GetTreeAsync();
        Task InsertFirstRootNode(SettingFeature entity);
        Task InsertLastRootNode(SettingFeature entity);
        Task InsertRootNodeBeforeAnother(SettingFeature entity, Guid referenceNodeId);
        Task InsertRootNodeAfterAnother(SettingFeature entity, Guid referenceNodeId);
        Task InsertFirstChildNode(SettingFeature entity, Guid referenceNodeId);
        Task InsertLastChildNode(SettingFeature entity, Guid referenceNodeId);
        Task InsertNodeBeforeAnother(SettingFeature entity, Guid referenceNodeId);
        Task InsertNodeAfterAnother(SettingFeature entity, Guid referenceNodeId);
        void RemoveNode(Guid id);
    }
}