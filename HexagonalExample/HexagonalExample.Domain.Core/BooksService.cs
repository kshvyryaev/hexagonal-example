using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Contracts.Services;
using HexagonalExample.Domain.Entities;

namespace HexagonalExample.Domain.Core
{
    public class BooksService : IBooksService
    {
        #region Constants

        private const string AllBooksKeyForCache = nameof(Book) + nameof(GetAllAsync);

        #endregion Constants

        #region Fields

        private readonly IValidatorAdapter<Book> _bookValidator;
        private readonly ICacheAdapter _cacheAdapter;
        private readonly IBooksRepository _booksRepository;

        #endregion Fields

        #region Constructors

        public BooksService(
            IValidatorAdapter<Book> bookValidator,
            ICacheAdapter cacheAdapter,
            IBooksRepository booksRepository)
        {
            _bookValidator = bookValidator ?? throw new ArgumentNullException(nameof(bookValidator));
            _cacheAdapter = cacheAdapter ?? throw new ArgumentNullException(nameof(cacheAdapter));
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        #endregion Constructors

        #region Methods

        public async Task<Book> CreateAsync(Book book)
        {
            _bookValidator.ValidateAndThrowIfFailed(book);

            Book createdBook = await _booksRepository.CreateAsync(book);
            _cacheAdapter.Set(createdBook.GetKeyForCache(), createdBook);
            _cacheAdapter.Delete(AllBooksKeyForCache);

            return createdBook;
        }

        public async Task<Book> GetAsync(string id)
        {
            string cacheKey = Book.GetKeyForCache(id);
            var book = _cacheAdapter.Get<Book>(cacheKey);

            if (book == null)
            {
                book = await _booksRepository.GetAsync(id);

                if (book != null)
                {
                    _cacheAdapter.Set(book.GetKeyForCache(), book);
                }
            }

            return book;
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync()
        {
            var books = _cacheAdapter.Get<IReadOnlyCollection<Book>>(AllBooksKeyForCache);

            if (books == null)
            {
                books = await _booksRepository.GetAllAsync();
                _cacheAdapter.Set(AllBooksKeyForCache, books);
            }

            return books;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _bookValidator.ValidateAndThrowIfFailed(book);

            Book updatedBook = await _booksRepository.UpdateAsync(book);
            _cacheAdapter.Update(updatedBook.GetKeyForCache(), updatedBook);
            _cacheAdapter.Delete(AllBooksKeyForCache);

            return updatedBook;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool isDeleted = await _booksRepository.DeleteAsync(id);

            if (isDeleted)
            {
                string cacheKey = Book.GetKeyForCache(id);
                _cacheAdapter.Delete(cacheKey);
                _cacheAdapter.Delete(AllBooksKeyForCache);
            }

            return isDeleted;
        }

        #endregion Methods
    }
}
