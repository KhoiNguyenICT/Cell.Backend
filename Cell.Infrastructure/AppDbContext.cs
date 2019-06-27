using Cell.Core.Repositories;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}