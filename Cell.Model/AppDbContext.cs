using Cell.Common.SeedWork;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingApiEntity;
using Cell.Model.Entities.SettingFeatureEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Entities.SettingFilterEntity;
using Cell.Model.Entities.SettingFormEntity;
using Cell.Model.Entities.SettingReportEntity;
using Cell.Model.Entities.SettingTableEntity;
using Cell.Model.Entities.SettingViewEntity;
using Cell.Model.Entities.SystemLogEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cell.Model
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Guid CurrentSessionId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"] ?? throw new InvalidOperationException());
        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"] ?? throw new InvalidOperationException());

        public AppDbContext(
            DbContextOptions options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }
        public virtual DbSet<SecurityPermission> SecurityPermissions { get; set; }
        public virtual DbSet<SecuritySession> SecuritySessions { get; set; }
        public virtual DbSet<SecurityUser> SecurityUsers { get; set; }
        public virtual DbSet<SettingAction> SettingActions { get; set; }
        public virtual DbSet<SettingActionInstance> SettingActionInstances { get; set; }
        public virtual DbSet<SettingAdvanced> SettingAdvanceds { get; set; }
        public virtual DbSet<SettingApi> SettingApi { get; set; }
        public virtual DbSet<SettingFeature> SettingFeatures { get; set; }
        public virtual DbSet<SettingField> SettingFields { get; set; }
        public virtual DbSet<SettingFieldInstance> SettingFieldInstances { get; set; }
        public virtual DbSet<SettingFilter> SettingFilters { get; set; }
        public virtual DbSet<SettingForm> SettingForms { get; set; }
        public virtual DbSet<SettingReport> SettingReports { get; set; }
        public virtual DbSet<SettingTable> SettingTables { get; set; }
        public virtual DbSet<SettingView> SettingViews { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modified)
            {
                if (!(item.Entity is Entity changedOrAddedItem)) continue;
                switch (item.State)
                {
                    case EntityState.Added:
                        changedOrAddedItem.CreatedBy = CurrentAccountId;
                        changedOrAddedItem.Created = DateTimeOffset.Now;
                        changedOrAddedItem.Modified = DateTimeOffset.Now;
                        changedOrAddedItem.Modified = DateTimeOffset.Now;
                        changedOrAddedItem.ModifiedBy = CurrentAccountId;
                        changedOrAddedItem.Version = 0;
                        break;

                    case EntityState.Modified:
                        changedOrAddedItem.Modified = DateTimeOffset.Now;
                        changedOrAddedItem.ModifiedBy = Guid.Empty;
                        changedOrAddedItem.Version += 1;
                        break;

                    case EntityState.Deleted:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modified)
            {
                if (!(item.Entity is Entity changedOrAddedItem)) continue;
                if (item.State == EntityState.Added)
                {
                    changedOrAddedItem.CreatedBy = Guid.Empty;
                    changedOrAddedItem.Created = DateTimeOffset.Now;
                    changedOrAddedItem.Modified = DateTimeOffset.Now;
                    changedOrAddedItem.Modified = DateTimeOffset.Now;
                    changedOrAddedItem.ModifiedBy = Guid.Empty;
                    changedOrAddedItem.Version = 0;
                }

                changedOrAddedItem.Modified = DateTimeOffset.Now;
                changedOrAddedItem.ModifiedBy = Guid.Empty;
                changedOrAddedItem.Version += 1;
            }
            return base.SaveChanges();
        }
    }
}