using System.Collections.Generic;

namespace Cell.Helpers.Models
{
    public class DynamicSearchModel
    {
        public IEnumerable<Select> Select { get; set; }

        public IEnumerable<From> From { get; set; }

        public IEnumerable<Where> Where { get; set; }
    }

    public class Select
    {
        public string Table { get; set; }

        public string Field { get; set; }

        public string Alias { get; set; }
    }

    public class From
    {
        public string Table { get; set; }

        public string JoinTable { get; set; }

        public string JoinClause { get; set; }

        public IEnumerable<JoinCondition> JoinConditions { get; set; }
    }

    public class JoinCondition
    {
        public string Field { get; set; }

        public string JoinField { get; set; }
    }

    public class Where
    {
        public string Table { get; set; }

        public string Field { get; set; }

        public string DataType { get; set; }

        public string Value { get; set; }

        public string Function { get; set; }
    }
}