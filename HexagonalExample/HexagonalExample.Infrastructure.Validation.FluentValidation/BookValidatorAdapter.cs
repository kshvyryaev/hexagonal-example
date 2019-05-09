using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers;
using DomainValidation = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation
{
    public class BookValidatorAdapter : AbstractValidator<Book>, IValidatorAdapter<Book>
    {
        #region Constants

        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 150;
        private const string BookDoesNotExistsMessage = "Book with this id does not exist.";

        #endregion Constants

        #region Fields

        private readonly IValidatorAdapter<Author> _authorValidator;

        #endregion Fields

        #region Constructors

        public BookValidatorAdapter(IValidatorAdapter<Author> authorValidator, IBooksRepository booksRepository)
        {
            _authorValidator = authorValidator ?? throw new ArgumentNullException(nameof(authorValidator));

            RuleFor(x => x.Id)
                .Must(id =>
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var storedBook = booksRepository.Get(id);

                        if (storedBook == null)
                        {
                            return false;
                        }
                    }

                    return true;
                })
                .WithMessage(BookDoesNotExistsMessage);

            RuleFor(x => x.Name).NotEmpty().Length(0, NameMaxLength);
            RuleFor(x => x.Description).Length(0, DescriptionMaxLength);
        }

        #endregion Constructors

        #region Methods

        public IReadOnlyCollection<DomainValidation.ValidationError> ValidateEntity(Book book)
        {
            var errors = new List<DomainValidation.ValidationError>();

            ValidationResult result = Validate(book);
            var bookErrors = result.Errors
                .ParseToValidationErrors()
                .SetEntityNameForAllItems(nameof(Book));

            errors.AddRange(bookErrors);

            foreach (var author in book.Authors)
            {
                errors.AddRange(_authorValidator.ValidateEntity(author));
            }

            return errors;
        }

        public void ValidateEntityAndThrow(Book book)
        {
            var errors = ValidateEntity(book);
            if (errors.Count > 0)
            {
                throw new DomainValidation.ValidationException(errors);
            }
        }

        #endregion Methods
    }
}
