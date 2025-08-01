using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Контроллер для управления операциями, связанными с заказами.
    /// </summary>
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получает список заказов.
        /// </summary>
        /// <returns>Список всех заказов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Получает список заказов с необязательными фильтрами.
        /// </summary>
        /// <param name="orderId">Фильтр по идентификатору заказа.</param>
        /// <param name="orderDate">Фильтр по дате заказа.</param>
        /// <returns>Список заказов.</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersFilter([FromQuery] int? orderId, [FromQuery] DateTime? orderDate)
        {
            var orders = await _orderService.GetAllOrdersFilterAsync(orderId, orderDate);
            return Ok(orders);
        }

        /// <summary>
        /// Получает детали заказа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Детали заказа.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Создает новый заказ.
        /// </summary>
        /// <param name="orderCreateDto">Данные заказа.</param>
        /// <returns>Созданный заказ.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(orderCreateDto);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}