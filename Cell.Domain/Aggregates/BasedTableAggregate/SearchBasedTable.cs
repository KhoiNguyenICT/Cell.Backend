using System;

namespace Cell.Domain.Aggregates.BasedTableAggregate
{
    public class SearchBasedTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Used { get; set; }
        public string Code { get; set; }
    }
}