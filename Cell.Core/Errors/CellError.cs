﻿using System.Collections.Generic;
using System.Linq;

namespace Cell.Core.Errors
{
    public class CellError
    {
        private readonly string _message;
        private readonly List<string> _messages;

        public int StatusCode { get; set; }

        public IDictionary<string, IEnumerable<string>> ValidationErrors { get; set; }

        public IEnumerable<string> Messages
        {
            get
            {
                if (ValidationErrors == null || !ValidationErrors.Any())
                    return !string.IsNullOrEmpty(_message) ? new List<string> { _message } :
                        _messages.Count > 0 ? new List<string>(_messages) : new List<string>();
                var listMessages = new List<string>();
                foreach (var err in ValidationErrors)
                {
                    if (err.Value != null && err.Value.Any())
                    {
                        listMessages.AddRange(err.Value);
                    }
                }
                return listMessages;
            }
        }

        public CellError(int statusCode = 400)
        {
            ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            StatusCode = statusCode;
        }

        public CellError(string message, int statusCode = 400)
        {
            _message = message;
            StatusCode = statusCode;
        }

        public CellError(List<string> messages, int statusCode = 400)
        {
            _messages = messages;
            StatusCode = statusCode;
        }

        public CellError(IEnumerable<CellValidationError> validationErrors, int statusCode = 400)
        {
            AddErrors(validationErrors);
            StatusCode = statusCode;
        }

        public void AddError(CellValidationError validationError)
        {
            if (ValidationErrors == null)
            {
                ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            }
            var fieldName = validationError.Field ?? "Generic";
            if (ValidationErrors.ContainsKey(fieldName))
            {
                var value = ValidationErrors[fieldName];
                var enumerable = value as string[] ?? value.ToArray();
                if (value != null && enumerable.Any())
                {
                    enumerable.Append(validationError.Message);
                }
                else
                {
                    ValidationErrors[fieldName] = new List<string> { validationError.Message };
                }
            }
            else
            {
                ValidationErrors[fieldName] = new List<string> { validationError.Message };
            }
        }

        public void AddErrors(IEnumerable<CellValidationError> validationErrors)
        {
            var errors = validationErrors as IList<CellValidationError> ?? validationErrors.ToList();
            if (!errors.Any()) return;
            foreach (var err in errors)
            {
                AddError(err);
            }
        }
    }
}