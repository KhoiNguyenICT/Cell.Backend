using Cell.Core.Enums;

namespace Cell.Domain.Aggregates.BasedTableAggregate
{
    public class AddColumnBasedTable
    {
        public string Table { get; set; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public int DataSize { get; set; }
    }
}