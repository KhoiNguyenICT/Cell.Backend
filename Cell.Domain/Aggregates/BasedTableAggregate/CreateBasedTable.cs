namespace Cell.Domain.Aggregates.BasedTableAggregate
{
    public class CreateBasedTable
    {
        public string TableName { get; set; }
        public bool IsTree { get; set; }
    }
}