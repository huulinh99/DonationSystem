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
    public class GiftDetailsController : ControllerBase
    {
        private readonly SWD391Context _context;

        public GiftDetailsController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/GiftDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDetails>>> GetGiftDetails()
        {
            return await _context.GiftDetails.ToListAsync();
        }

        // GET: api/GiftDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDetails>> GetGiftDetails(int id)
        {
            var giftDetails = await _context.GiftDetails.FindAsync(id);

            if (giftDetails == null)
            {
                return NotFound();
            }

            return giftDetails;
        }

        // PUT: api/GiftDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiftDetails(int id, GiftDetails giftDetails)
        {
            if (id != giftDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(giftDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiftDetailsExists(id))
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

        // POST: api/GiftDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GiftDetails>> PostGiftDetails(GiftDetails giftDetails)
        {
            _context.GiftDetails.Add(giftDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GiftDetailsExists(giftDetails.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGiftDetails", new { id = giftDetails.Id }, giftDetails);
        }

        // DELETE: api/GiftDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GiftDetails>> DeleteGiftDetails(int id)
        {
            var giftDetails = await _context.GiftDetails.FindAsync(id);
            if (giftDetails == null)
            {
                return NotFound();
            }

            _context.GiftDetails.Remove(giftDetails);
            await _context.SaveChangesAsync();

            return giftDetails;
        }

        private bool GiftDetailsExists(int id)
        {
            return _context.GiftDetails.Any(e => e.Id == id);
        }
    }
}
