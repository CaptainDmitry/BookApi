using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Контроллер для управления операциями, связанными с книгами.
    /// </summary>
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Получает список книг.
        /// </summary>
        /// <returns>Список всех книг.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }


        /// <summary>
        /// Получает список книг с необязательными фильтрами.
        /// </summary>
        /// <param name="title">Фильтр по названию книги.</param>
        /// <param name="publicationDate">Фильтр по дате публикации.</param>
        /// <returns>Список книг.</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByFilter([FromQuery] string title, [FromQuery] DateTime? publicationDate)
        {
            var books = await _bookService.GetAllBooksFilterAsync(title, publicationDate);
            return Ok(books);
        }

        /// <summary>
        /// Получает полную информацию о книге по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Детали книги.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        /// Добавляет новую книгу.
        /// </summary>
        /// <param name="bookCreateDto">Данные книги.</param>
        /// <returns>Созданная книга.</returns>
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook([FromBody] BookCreateDto bookCreateDto)
        {
            var book = await _bookService.AddBookAsync(bookCreateDto);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления.</param>
        /// <param name="bookUpdateDto">Обновленные данные книги.</param>
        /// <returns>204 No Content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookCreateDto bookUpdateDto)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, bookUpdateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Удаляет книгу по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления.</param>
        /// <returns>204 No Content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}