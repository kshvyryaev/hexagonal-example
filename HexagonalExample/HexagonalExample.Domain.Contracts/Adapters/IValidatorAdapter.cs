using System.Collections.Generic;
using HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Domain.Contracts.Adapters
{
    public interface IValidatorAdapter<TEntity>
        where TEntity: class, new()
    {
        IReadOnlyCollection<ValidationError> ValidateEntity(TEntity entity);

        void ValidateEntityAndThrow(TEntity entity);
    }
}
