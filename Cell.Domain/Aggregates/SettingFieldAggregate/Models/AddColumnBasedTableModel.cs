namespace Cell.Domain.Aggregates.SettingFieldAggregate.Models
{
    public class AddColumnBasedTableModel
    {
        public string Table { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public object DataSize { get; set; }
    }
}