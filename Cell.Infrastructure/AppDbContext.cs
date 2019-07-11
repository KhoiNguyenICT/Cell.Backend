using Cell.Core.Repositories;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Cell.Domain.Aggregates.SettingFilterAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingReportAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.SettingElasticSearch;

namespace Cell.Infrastructure
{
    public class AppDbContext : BaseDbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<SettingTable> SettingTables { get; set; }
        public DbSet<SettingField> SettingFields { get; set; }
        public DbSet<SettingAction> SettingActions { get; set; }
        public DbSet<SettingForm> SettingForms { get; set; }
        public DbSet<SettingView> SettingViews { get; set; }
        public DbSet<SettingFeature> SettingFeatures { get; set; }
        public DbSet<SettingFieldInstance> SettingFieldInstances { get; set; }
        public DbSet<SettingActionInstance> SettingActionInstances { get; set; }
        public DbSet<SettingAdvanced> SettingAdvanceds { get; set; }
        public DbSet<SettingFilter> SettingFilters { get; set; }
        public DbSet<SettingReport> SettingReports { get; set; }
        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<SecurityPermission> SecurityPermissions { get; set; }
        public DbSet<SecuritySession> SecuritySessions { get; set; }
        public DbSet<SettingElasticSearch> SettingElasticSearches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modified)
            {
                if (!(item.Entity is IEntity changedOrAddedItem)) continue;
                if (item.State == EntityState.Added)
                {
                    changedOrAddedItem.Created = DateTime.Now;
                }

                changedOrAddedItem.Modified = DateTime.Now;
                changedOrAddedItem.Version += 1;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}