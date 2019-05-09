using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.EF.Models;

namespace HexagonalExample.Infrastructure.Data.EF
{
    public class BooksRepository : IBooksRepository
    {
        #region Fields

        private readonly IMapperAdapter _mapperAdapter;
        private readonly DatabaseContext _context;

        #endregion Fields

        #region Constructors

        public BooksRepository(IMapperAdapter mapperAdapter, DatabaseContext context)
        {
            _mapperAdapter = mapperAdapter ?? throw new ArgumentNullException(nameof(mapperAdapter));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Constructors

        #region Methods

        public async Task<Book> CreateAsync(Book book)
        {
            var bookModel = _mapperAdapter.Map<Book, BookEFModel>(book);
            _context.Books.Add(bookModel);

            if (await _context.SaveChangesAsync() == 0)
            {
                return null;
            }

            return _mapperAdapter.Map<BookEFModel, Book>(bookModel);
        }

        public async Task<Book> GetAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
            {
                return null;
            }

            BookEFModel bookModel = await _context.Books
                .Include(x => x.Authors)
                .FirstOrDefaultAsync(x => x.Id == parsedId);

            if (bookModel == null)
            {
                return null;
            }

            return _mapperAdapter.Map<BookEFModel, Book>(bookModel);
        }

        public Book Get(string id)
        {
            if (!int.TryParse(id, out int parsedId))
            {
                return null;
            }

            BookEFModel bookModel = _context.Books
                .Include(x => x.Authors)
                .FirstOrDefault(x => x.Id == parsedId);

            if (bookModel == null)
            {
                return null;
            }

            return _mapperAdapter.Map<BookEFModel, Book>(bookModel);
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync()
        {
            List<BookEFModel> bookModels = await _context.Books
                .Include(x => x.Authors)
                .ToListAsync();

            return _mapperAdapter.Map<IReadOnlyCollection<Book>>(bookModels);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var bookModel = _mapperAdapter.Map<Book, BookEFModel>(book);
            _context.Entry(bookModel).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() == 0)
            {
                return null;
            }

            return _mapperAdapter.Map<BookEFModel, Book>(bookModel);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
            {
                return false;
            }

            var bookModel = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == parsedId);

            if (bookModel == null)
            {
                return false;
            }

            _context.Books.Remove(bookModel);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion Methods
    }
}
