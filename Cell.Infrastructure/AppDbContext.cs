using Cell.Core.Repositories;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cell.Infrastructure
{
    public class AppDbContext : BaseDbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<SettingTable> SettingTables { get; set; }
        public DbSet<SettingField> SettingFields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}