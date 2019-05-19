using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using DomainValidation = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers
{
    internal static class ErrorsParser
    {
        internal static IReadOnlyCollection<DomainValidation.ValidationError> ParseToValidationErrors(
            this IEnumerable<ValidationFailure> fluentValidationErrors)
        {
            return fluentValidationErrors
                .AsParallel()
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
            Parallel.ForEach(validationErrors, x => x.EntityName = entityName);

            return validationErrors;
        }
    }
}
