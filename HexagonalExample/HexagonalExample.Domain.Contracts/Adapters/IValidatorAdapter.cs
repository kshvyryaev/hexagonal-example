namespace HexagonalExample.Domain.Contracts.Adapters
{
    public interface IValidatorAdapter<TEntity>
        where TEntity: class, new()
    {
        void ValidateAndThrowIfFailed(TEntity entity);
    }
}
