using System;

namespace HexagonalExample.Domain.Entities.Validation
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; }

        public string ErrorMessage { get; }
    }
}
