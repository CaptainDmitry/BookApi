using AutoMapper;
using BookApi.Entities;
using BookApi.Models;
using BookApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersFilterAsync(int? orderId, DateTime? orderDate)
        {
            var orders = await _orderRepository.GetOrdersByFilterAsync(orderId, orderDate);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderDetailsAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var orderEntity = new Order { OrderDate = DateTime.UtcNow };
            orderEntity.OrderItems = new List<OrderItem>();

            foreach (var itemDto in orderCreateDto.OrderItems)
            {
                var book = await _bookRepository.GetByIdAsync(itemDto.BookId);
                if (book == null)
                {
                    throw new KeyNotFoundException($" нига по ID {itemDto.BookId} не найдена.");
                }

                orderEntity.OrderItems.Add(new OrderItem
                {
                    BookId = itemDto.BookId,
                    Quantity = itemDto.Quantity,
                    Book = book
                });
            }

            await _orderRepository.AddAsync(orderEntity);
            return _mapper.Map<OrderDto>(orderEntity);
        }
    }
}