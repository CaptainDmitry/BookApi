using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}