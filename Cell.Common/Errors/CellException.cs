using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Cell.Common.Errors;

namespace Cell.Core.Errors
{
    public class CellException : Exception
    {
        public CellError Error { get; set; }

        public string SerializedErrors => JsonConvert.SerializeObject(Error, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        public CellException()
        {
        }

        public CellException(ValidationResult result)
        {
            Error = new CellError(new List<string>(result.Errors.Select(x => x.ErrorMessage).ToList()));
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