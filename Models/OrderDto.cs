using System;
using System.Collections.Generic;

namespace BookApi.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}