using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> GetAllBooksFilterAsync(string title, DateTime? publicationDate);
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> AddBookAsync(BookCreateDto bookCreateDto);
        Task UpdateBookAsync(int id, BookCreateDto bookUpdateDto);
        Task DeleteBookAsync(int id);
    }
}