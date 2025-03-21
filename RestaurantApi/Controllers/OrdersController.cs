using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.Dtos;

namespace RestaurantApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var table = await _context.Tables.FindAsync(createOrderDto.TableId);
            if (table == null)
                return BadRequest("Invalid TableId");

            // Map DTO to Order Entity
            var newOrder = new Order
            {
                TableId = createOrderDto.TableId,
                OrderItems = createOrderDto.Items.Select(itemDto => new OrderItem
                {
                    MenuItemId = itemDto.MenuItemId,
                    Quantity = itemDto.Quantity,
                }).ToList()
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest();

            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                return NotFound();

            // Update order details
            existingOrder.TableId = updatedOrder.TableId;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.OrderItems = updatedOrder.OrderItems;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
