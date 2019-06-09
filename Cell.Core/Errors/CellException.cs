using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cell.Core.Errors
{
    public class CellException : Exception
    {
        public CellError Error { get; set; }

        public string SerializedErrors => JsonConvert.SerializeObject(Error, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        public CellException()
        {
        }

        public CellException(string field, string message)
        {
            Error = new CellError(new List<CellValidationError> { new CellValidationError(field, message) });
        }

        public CellException(string message)
        {
            Error = new CellError(new List<CellValidationError> { new CellValidationError(message) });
        }

        public CellException(CellValidationError validationError)
        {
            Error = new CellError(new List<CellValidationError> { validationError });
        }

        public CellException(IEnumerable<CellValidationError> validationErrors)
        {
            Error = new CellError(validationErrors);
        }
    }
}
