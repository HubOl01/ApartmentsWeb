using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<IActionResult> GetCitiesWithHouseCount()
        {
            var result = await _context.Cities
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    HouseCount = c.Streets.SelectMany(s => s.Houses).Count()
                })
                .ToListAsync();

            return Ok(result);
        }
        // GET: api/Cities/5/streets
        [HttpGet("{cityId}/streets")]
        public async Task<IActionResult> GetStreetsByCity(int cityId)
        {
            var result = await _context.Streets
                .Where(s => s.CityId == cityId)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    HouseCount = s.Houses.Count
                })
                .ToListAsync();

            return Ok(result);
        }
        // GET: api/Cities51/houses
        [HttpGet("{cityId}/houses")]
        public async Task<IActionResult> GetHousesByCity(int cityId)
        {
            var result = await _context.Houses
                .Where(h => h.Street.CityId == cityId)
                .Include(h => h.Street)
                .ThenInclude(s => s.City)
                .Select(h => new
                {
                    h.Id,
                    Address = $"{h.Street.City.Name}, {h.Street.Name}, д. {h.Number}",
                    ApartmentCount = h.Apartments.Count
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
          if (_context.Cities == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
          }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
