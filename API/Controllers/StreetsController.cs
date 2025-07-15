using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StreetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Streets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreets()
        {
            return await _context.Streets.Include(e => e.Houses).ToListAsync();
        }

        [HttpGet("{streetId}/houses")]
        public async Task<IActionResult> GetHousesByStreet(int streetId)
        {
            var result = await _context.Houses
                .Where(h => h.StreetId == streetId)
                .Include(h => h.Street)
                .ThenInclude(s => s!.City)
                .Select(h => new
                {
                    h.Id,
                    Address = $"{h.Street!.City!.Name}, {h.Street.Name}, д. {h.Number}",
                    ApartmentCount = h.Apartments.Count
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Streets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Street>> GetStreet(int id)
        {
            var street = await _context.Streets.FindAsync(id);

            if (street == null)
            {
                return NotFound();
            }

            return street;
        }

        // PUT: api/Streets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreet(int id, Street street)
        {
            if (id != street.Id)
            {
                return BadRequest();
            }

            _context.Entry(street).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Streets
        [HttpPost]
        public async Task<ActionResult<Street>> PostStreet(Street street)
        {
            _context.Streets.Add(street);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStreet", new { id = street.Id }, street);
        }

        // DELETE: api/Streets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreetExists(int id)
        {
            return (_context.Streets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}