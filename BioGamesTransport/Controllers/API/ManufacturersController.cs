using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioGamesTransport.Data.SQL;

namespace BioGamesTransport.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly BiogamesTransContext _context;

        public ManufacturersController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturers>>> GetManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturers>> GetManufacturers(int id)
        {
            var manufacturers = await _context.Manufacturers.FindAsync(id);

            if (manufacturers == null)
            {
                return NotFound();
            }

            return manufacturers;
        }

        // PUT: api/Manufacturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturers(int id, Manufacturers manufacturers)
        {
            if (id != manufacturers.Id)
            {
                return BadRequest();
            }

            _context.Entry(manufacturers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturersExists(id))
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

        // POST: api/Manufacturers
        [HttpPost]
        public async Task<ActionResult<Manufacturers>> PostManufacturers(Manufacturers manufacturers)
        {
            _context.Manufacturers.Add(manufacturers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManufacturers", new { id = manufacturers.Id }, manufacturers);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Manufacturers>> DeleteManufacturers(int id)
        {
            var manufacturers = await _context.Manufacturers.FindAsync(id);
            if (manufacturers == null)
            {
                return NotFound();
            }

            _context.Manufacturers.Remove(manufacturers);
            await _context.SaveChangesAsync();

            return manufacturers;
        }

        private bool ManufacturersExists(int id)
        {
            return _context.Manufacturers.Any(e => e.Id == id);
        }
    }
}
