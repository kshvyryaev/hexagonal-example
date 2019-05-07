using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Data.Mongo
{
    public class BooksRepository : RepositoryBase<Book, BookMongoModel>, IBooksRepository
    {
        #region Fields

        private readonly IMapperAdapter _mapperAdapter;

        #endregion Fields

        #region Constructors

        public BooksRepository(IMapperAdapter mapperAdapter)
        {
            _mapperAdapter = mapperAdapter ?? throw new ArgumentNullException(nameof(mapperAdapter));
        }

        #endregion Constructors

        #region Public methods

        public async Task<Book> CreateAsync(Book book)
        {
            return await SaveAsync(book);
        }

        public async Task<Book> GetAsync(string id)
        {
            BookMongoModel bookModel = await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return _mapperAdapter.Map<BookMongoModel, Book>(bookModel);
        }

        public Book Get(string id)
        {
            BookMongoModel bookModel = Collection.Find(x => x.Id.Equals(id)).FirstOrDefault();
            return _mapperAdapter.Map<BookMongoModel, Book>(bookModel);
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync()
        {
            List<BookMongoModel> bookModels = await Collection.Find(x => true).ToListAsync();
            return _mapperAdapter.Map<IReadOnlyCollection<Book>>(bookModels);
        }

        public IReadOnlyCollection<Book> GetAll()
        {
            List<BookMongoModel> bookModels = Collection.Find(x => true).ToList();
            return _mapperAdapter.Map<IReadOnlyCollection<Book>>(bookModels);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            return await SaveAsync(book);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            DeleteResult deleteResult = await Collection.DeleteOneAsync(e => e.Id.Equals(id));
            return deleteResult.DeletedCount > 0;
        }

        #endregion Public methods

        #region Private methods

        private async Task<Book> SaveAsync(Book book)
        {
            var bookModel = _mapperAdapter.Map<Book, BookMongoModel>(book);
            bookModel.SetMissedIdentifiers();

            await Collection.ReplaceOneAsync(e => e.Id.Equals(bookModel.Id), bookModel, new UpdateOptions
            {
                IsUpsert = true
            });

            return _mapperAdapter.Map<BookMongoModel, Book>(bookModel);
        }

        #endregion Private methods
    }
}
