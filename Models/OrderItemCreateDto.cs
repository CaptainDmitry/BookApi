namespace BookApi.Models
{
    public class OrderItemCreateDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}