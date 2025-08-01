using AutoMapper;
using BookApi.Entities;
using BookApi.Models;
using BookApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksFilterAsync(string title, DateTime? publicationDate)
        {
            var books = await _bookRepository.GetBooksByFilterAsync(title, publicationDate);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddBookAsync(BookCreateDto bookCreateDto)
        {
            var bookEntity = _mapper.Map<Book>(bookCreateDto);
            await _bookRepository.AddAsync(bookEntity);
            return _mapper.Map<BookDto>(bookEntity);
        }

        public async Task UpdateBookAsync(int id, BookCreateDto bookUpdateDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException(" нига не найдена");
            }

            _mapper.Map(bookUpdateDto, existingBook);
            await _bookRepository.UpdateAsync(existingBook);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}