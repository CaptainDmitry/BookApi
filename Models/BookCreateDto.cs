using System;
using System.Collections.Generic;

namespace BookApi.Models
{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
    }
}