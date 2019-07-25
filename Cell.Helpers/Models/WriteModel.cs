using System;
using System.Collections.Generic;

namespace Cell.Helpers.Models
{
    public abstract class WriteModel
    {
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    public class OutputQuery
    {
        public string Query { get; set; }
        public List<KeyValuePair<string, object>> Parameters { get; set; }
    }
}