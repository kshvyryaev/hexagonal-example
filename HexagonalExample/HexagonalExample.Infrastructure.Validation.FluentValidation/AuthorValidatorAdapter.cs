using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers;
using DomainValidation = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation
{
    public class AuthorValidatorAdapter : AbstractValidator<Author>, IValidatorAdapter<Author>
    {
        #region Constants

        private const int NameMaxLength = 50;
        private const int SurnameMaxLength = 50;

        #endregion Constants

        #region Constructors

        public AuthorValidatorAdapter()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, NameMaxLength);
            RuleFor(x => x.Surname).Length(0, SurnameMaxLength);
        }

        #endregion Constructors

        #region Methods

        public IReadOnlyCollection<DomainValidation.ValidationError> ValidateEntity(Author author)
        {
            ValidationResult result = Validate(author);
            var authorErrors = result.Errors
                .ParseToValidationErrors()
                .SetEntityNameForAllItems(nameof(Author));

            return authorErrors;
        }

        public void ValidateEntityAndThrow(Author author)
        {
            var errors = ValidateEntity(author);
            if (errors.Count > 0)
            {
                throw new DomainValidation.ValidationException(errors);
            }
        }

        #endregion Methods
    }
}
