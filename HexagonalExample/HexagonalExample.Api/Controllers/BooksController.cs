using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HexagonalExample.Api.Models.Commands.Books;
using HexagonalExample.Api.Models.Responses;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Services;
using HexagonalExample.Domain.Entities;

namespace HexagonalExample.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Fields

        private readonly IMapperAdapter _mapperAdapter;
        private readonly IBooksService _booksService;

        #endregion Fields

        #region Constrictors

        public BooksController(IMapperAdapter mapperAdapter, IBooksService booksService)
        {
            _mapperAdapter = mapperAdapter ?? throw new ArgumentNullException(nameof(mapperAdapter));
            _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
        }

        #endregion Constrictors

        #region Methods

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand createBookCommand)
        {
            var book = _mapperAdapter.Map<CreateBookCommand, Book>(createBookCommand);
            Book createdBook = await _booksService.CreateAsync(book);

            var response = _mapperAdapter.Map<Book, BookResponse>(createdBook);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Book book = await _booksService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var response = _mapperAdapter.Map<Book, BookResponse>(book);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<Book> books = await _booksService.GetAllAsync();

            var response = _mapperAdapter.Map<IReadOnlyCollection<BookResponse>>(books);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBookCommand updateBookCommand)
        {
            var book = _mapperAdapter.Map<UpdateBookCommand, Book>(updateBookCommand);
            Book updatedBook = await _booksService.UpdateAsync(book);

            if (updatedBook == null)
            {
                return NotFound();
            }

            var response = _mapperAdapter.Map<Book, BookResponse>(book);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted = await _booksService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion Methods
    }
}