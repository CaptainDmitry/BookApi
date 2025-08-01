using System.Collections.Generic;

namespace BookApi.Models
{
    public class OrderCreateDto
    {
        public ICollection<OrderItemCreateDto> OrderItems { get; set; }
    }
}