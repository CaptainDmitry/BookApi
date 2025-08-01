using BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByFilterAsync(string title, DateTime? publicationDate);
    }
}