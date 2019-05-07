using MongoDB.Driver;

namespace HexagonalExample.Infrastructure.Data.Mongo
{
    public abstract class RepositoryBase<TEntity, TModel>
    {
        #region Constructors

        public RepositoryBase()
        {
            Database = DatabaseConfiguration.Database;
        }

        #endregion Constructors

        #region Properties

        protected IMongoDatabase Database { get; }

        protected IMongoCollection<TModel> Collection => Database.GetCollection<TModel>(typeof(TEntity).Name);

        #endregion Properties
    }
}
