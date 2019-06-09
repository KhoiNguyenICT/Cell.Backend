using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cell.Core.Extensions;
using Cell.Core.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cell.Core.Repositories
{
    public class BaseDbContext : DbContext, IUnitOfWork
    {
        protected readonly IMediator Mediator;

        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await Mediator.DispatchDomainEventsAsync(this);
            return result;
        }
    }
}
