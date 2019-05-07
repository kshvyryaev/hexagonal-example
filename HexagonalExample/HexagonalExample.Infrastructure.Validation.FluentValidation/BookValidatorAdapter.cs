using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Validation.FluentValidation.Helpers;
using DomainValidationEntities = HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Infrastructure.Validation.FluentValidation
{
    public class BookValidatorAdapter : AbstractValidator<Book>, IValidatorAdapter<Book>
    {
        #region Constants

        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 150;

        private const string BookDoesNotExistsMessage = "Book with this id does not exist.";
        private const string AuthorNameIsEmptyMessage = "Name of book author can not be null or empty.";

        #endregion Constants

        public BookValidatorAdapter(IBooksRepository booksRepository)
        {
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

            RuleForEach(x => x.Authors)
                .Must(author =>
                {
                    if (string.IsNullOrEmpty(author.Name))
                    {
                        return false;
                    }

                    return true;
                })
                .WithMessage(AuthorNameIsEmptyMessage);
        }

        public void ValidateAndThrowIfFailed(Book book)
        {
            ValidationResult result = Validate(book);

            if (!result.IsValid)
            {
                IEnumerable<DomainValidationEntities.ValidationError> errors =
                    result.Errors.ParseToValidationErrors();

                throw new DomainValidationEntities.ValidationException(errors);
            }
        }
    }
}
