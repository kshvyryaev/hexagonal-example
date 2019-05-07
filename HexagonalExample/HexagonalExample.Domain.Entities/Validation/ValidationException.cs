using System;
using System.Collections.Generic;

namespace HexagonalExample.Domain.Entities.Validation
{
    public class ValidationException : Exception
    {
        private const string ExceptionMessage = "Validation exception has occurred.";

        public ValidationException(IEnumerable<ValidationError> errors)
            : base(ExceptionMessage)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
