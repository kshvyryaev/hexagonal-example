using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using DomainValidationEntities = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers
{
    internal static class ErrorsParser
    {
        internal static IEnumerable<DomainValidationEntities.ValidationError> ParseToValidationErrors(
            this IEnumerable<ValidationFailure> fluentValidationErrors)
        {
            if (fluentValidationErrors == null)
            {
                throw new ArgumentNullException(nameof(fluentValidationErrors));
            }

            return fluentValidationErrors
                .Select(x => new DomainValidationEntities.ValidationError(x.PropertyName, x.ErrorMessage))
                .ToList();
        }
    }
}
