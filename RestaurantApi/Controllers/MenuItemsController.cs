using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers
{
    [Route("api/menuitems")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);

            if(menuItem == null)
                return NotFound();

            return Ok(menuItem);

        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.Id }, menuItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if(id!= menuItem.Id)
                return BadRequest("ID in URL and body do not match");

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);

            if(menuItem == null) 
                return NotFound();

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(x => x.Id == id);
        }
    }
}
