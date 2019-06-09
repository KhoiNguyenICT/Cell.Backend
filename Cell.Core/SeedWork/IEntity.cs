using System;

namespace Cell.Core.SeedWork
{
    public interface IEntity
    {
        Guid Id { get; set; }

        string Code { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        string Data { get; set; }

        DateTimeOffset Created { get; set; }

        Guid CreatedBy { get; set; }

        DateTimeOffset Modified { get; set; }

        Guid ModifiedBy { get; set; }

        int Version { get; set; }
    }
}