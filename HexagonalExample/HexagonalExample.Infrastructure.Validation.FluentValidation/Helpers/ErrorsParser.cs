using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using DomainValidation = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers
{
    internal static class ErrorsParser
    {
        internal static IReadOnlyCollection<DomainValidation.ValidationError> ParseToValidationErrors(
            this IEnumerable<ValidationFailure> fluentValidationErrors)
        {
            if (fluentValidationErrors == null)
            {
                throw new ArgumentNullException(nameof(fluentValidationErrors));
            }

            return fluentValidationErrors
                .Select(x => new DomainValidation.ValidationError
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                    AttemptedValue = x.AttemptedValue
                })
                .ToList();
        }

        internal static IReadOnlyCollection<DomainValidation.ValidationError> SetEntityNameForAllItems(
            this IReadOnlyCollection<DomainValidation.ValidationError> validationErrors,
            string entityName)
        {
            foreach (var error in validationErrors)
            {
                error.EntityName = entityName;
            }

            return validationErrors;
        }
    }
}
