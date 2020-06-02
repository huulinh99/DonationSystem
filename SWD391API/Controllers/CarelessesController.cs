using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD391API.Models;

namespace SWD391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarelessesController : ControllerBase
    {
        private readonly SWD391Context _context;

        public CarelessesController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/Carelesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carelesses>>> GetCarelesses()
        {
            var careless = _context.Carelesses
                           .OrderByDescending(c => c.Count)
                           .Take(5)
                           .ToList();
            return careless;
        }

        // GET: api/Carelesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Carelesses>>> GetCarelesses(string id)
        {
            var careless = _context.Carelesses
                           .Where(c=>c.UserId.Equals(id))
                           .OrderByDescending(c => c.Count)
                           .Take(3)
                           .ToList();
            return careless;
        }
        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Carelesses>>> MostPopular(int id)
        {
            var careless = _context.Carelesses
                           .OrderByDescending(c => c.Count)
                           .ToList();
            return careless;
        }

        // PUT: api/Carelesses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarelesses(int id, Carelesses carelesses)
        {
            if (id != carelesses.Id)
            {
                return BadRequest();
            }

            _context.Entry(carelesses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarelessesExists(id))
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

        // POST: api/Carelesses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Carelesses>> PostCarelesses(Carelesses carelesses)
        {
            _context.Carelesses.Add(carelesses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarelessesExists(carelesses.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarelesses", new { id = carelesses.Id }, carelesses);
        }

        // DELETE: api/Carelesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carelesses>> DeleteCarelesses(int id)
        {
            var carelesses = await _context.Carelesses.FindAsync(id);
            if (carelesses == null)
            {
                return NotFound();
            }

            _context.Carelesses.Remove(carelesses);
            await _context.SaveChangesAsync();

            return carelesses;
        }

        private bool CarelessesExists(int id)
        {
            return _context.Carelesses.Any(e => e.Id == id);
        }
    }
}
