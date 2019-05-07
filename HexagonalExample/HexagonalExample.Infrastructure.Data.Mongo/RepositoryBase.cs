using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Data.Mongo
{
    public abstract class RepositoryBase<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : MongoBaseModel
    {
        #region Fields

        private readonly IMapperAdapter _mapperAdapter;

        #endregion Fields

        #region Constructors

        public RepositoryBase(IMapperAdapter mapperAdapter)
        {
            _mapperAdapter = mapperAdapter ?? throw new ArgumentNullException(nameof(mapperAdapter));
            Database = DatabaseConfiguration.Database;
        }

        #endregion Constructors

        #region Properties

        protected IMongoDatabase Database { get; }

        protected IMongoCollection<TModel> Collection => Database.GetCollection<TModel>(typeof(TEntity).Name);

        #endregion Properties

        #region Public methods

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await SaveAsync(entity);
        }

        public async Task<TEntity> GetAsync(string id)
        {
            TModel model = await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return _mapperAdapter.Map<TModel, TEntity>(model);
        }

        public TEntity Get(string id)
        {
            TModel model = Collection.Find(x => x.Id.Equals(id)).FirstOrDefault();
            return _mapperAdapter.Map<TModel, TEntity>(model);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            List<TModel> models = await Collection.Find(x => true).ToListAsync();
            return _mapperAdapter.Map<IReadOnlyCollection<TEntity>>(models);
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            List<TModel> models = Collection.Find(x => true).ToList();
            return _mapperAdapter.Map<IReadOnlyCollection<TEntity>>(models);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await SaveAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            DeleteResult deleteResult = await Collection.DeleteOneAsync(e => e.Id.Equals(id));
            return deleteResult.DeletedCount > 0;
        }

        #endregion Public methods

        #region Private methods

        private async Task<TEntity> SaveAsync(TEntity entity)
        {
            var model = _mapperAdapter.Map<TEntity, TModel>(entity);
            model.SetMissedIdentifiers();

            await Collection.ReplaceOneAsync(e => e.Id.Equals(model.Id), model, new UpdateOptions
            {
                IsUpsert = true
            });

            return _mapperAdapter.Map<TModel, TEntity>(model);
        }

        #endregion Private methods
    }
}
