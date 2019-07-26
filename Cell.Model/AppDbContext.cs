using Cell.Common.SeedWork;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingFeatureEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Entities.SettingFilterEntity;
using Cell.Model.Entities.SettingFormEntity;
using Cell.Model.Entities.SettingReportEntity;
using Cell.Model.Entities.SettingTableEntity;
using Cell.Model.Entities.SettingViewEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cell.Model
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<SecurityPermission> SecurityPermissions { get; set; }
        public DbSet<SecuritySession> SecuritySessions { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<SettingAction> SettingActions { get; set; }
        public DbSet<SettingActionInstance> SettingActionInstances { get; set; }
        public DbSet<SettingAdvanced> SettingAdvanceds { get; set; }
        public DbSet<SettingFeature> SettingFeatures { get; set; }
        public DbSet<SettingField> SettingFields { get; set; }
        public DbSet<SettingFieldInstance> SettingFieldInstances { get; set; }
        public DbSet<SettingFilter> SettingFilters { get; set; }
        public DbSet<SettingForm> SettingForms { get; set; }
        public DbSet<SettingReport> SettingReports { get; set; }
        public DbSet<SettingTable> SettingTables { get; set; }
        public DbSet<SettingView> SettingViews { get; set; }

        private Guid CurrentSessionId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"]);

        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"]);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modified)
            {
                if (!(item.Entity is IEntity changedOrAddedItem)) continue;
                if (item.State == EntityState.Added)
                {
                    changedOrAddedItem.CreatedBy = Guid.Empty;
                    changedOrAddedItem.Created = DateTimeOffset.Now;
                    changedOrAddedItem.Modified = DateTimeOffset.Now;
                    changedOrAddedItem.Version = 0;
                }

                changedOrAddedItem.Modified = DateTimeOffset.Now;
                changedOrAddedItem.ModifiedBy = Guid.Empty;
                changedOrAddedItem.Version += 1;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modified)
            {
                if (!(item.Entity is IEntity changedOrAddedItem)) continue;
                if (item.State == EntityState.Added)
                {
                    changedOrAddedItem.CreatedBy = Guid.Empty;
                    changedOrAddedItem.Created = DateTimeOffset.Now;
                    changedOrAddedItem.Modified = DateTimeOffset.Now;
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