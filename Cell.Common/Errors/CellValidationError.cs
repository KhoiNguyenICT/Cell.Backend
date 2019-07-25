using System.Collections.Generic;
using Cell.Common.Extensions;

namespace Cell.Common.Errors
{
    public class CellValidationError
    {
        public CellValidationError(string field, string message)
        {
            Field = field?.ToCamelCasing();
            Message = message;
        }

        public List<string> Errors { get; set; }

        public CellValidationError(string message)
        {
            Message = message;
        }

        public string Field { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"Field = {Field} | Message = {Message}";
        }
    }
}