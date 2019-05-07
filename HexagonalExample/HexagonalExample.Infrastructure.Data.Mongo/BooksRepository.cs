using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Data.Mongo
{
    public class BooksRepository : RepositoryBase<Book, BookMongoModel>, IBooksRepository
    {
        public BooksRepository(IMapperAdapter mapperAdapter) : base(mapperAdapter)
        {
        }
    }
}
