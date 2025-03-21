using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TablesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
            var tables = await _context.Tables.ToListAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
                return NotFound();

            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTable), new { id = table.Id }, table);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            if (id != table.Id)
                return BadRequest("Id mismatch");

            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
                return NotFound();

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
