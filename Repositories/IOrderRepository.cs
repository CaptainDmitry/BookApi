using BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByFilterAsync(int? orderId, DateTime? orderDate);
        Task<Order> GetOrderDetailsAsync(int orderId);
    }
}