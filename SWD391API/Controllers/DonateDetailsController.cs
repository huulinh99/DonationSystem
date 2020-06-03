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
    public class DonateDetailsController : ControllerBase
    {
        private readonly SWD391Context _context;

        public DonateDetailsController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/DonateDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonateDetails>>> GetDonateDetails()
        {
            return await _context.DonateDetails.ToListAsync();
        }

        // GET: api/DonateDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonateDetails>> GetDonateDetails(int id)
        {
            var donateDetails = await _context.DonateDetails.FindAsync(id);

            if (donateDetails == null)
            {
                return NotFound();
            }

            return donateDetails;
        }

        // PUT: api/DonateDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonateDetails(int id, DonateDetails donateDetails)
        {
            if (id != donateDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(donateDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonateDetailsExists(id))
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

        // POST: api/DonateDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DonateDetails>> PostDonateDetails(DonateDetails donateDetails)
        {
            _context.DonateDetails.Add(donateDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DonateDetailsExists(donateDetails.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDonateDetails", new { id = donateDetails.Id }, donateDetails);
        }

        // DELETE: api/DonateDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonateDetails>> DeleteDonateDetails(int id)
        {
            var donateDetails = await _context.DonateDetails.FindAsync(id);
            if (donateDetails == null)
            {
                return NotFound();
            }

            _context.DonateDetails.Remove(donateDetails);
            await _context.SaveChangesAsync();

            return donateDetails;
        }

        private bool DonateDetailsExists(int id)
        {
            return _context.DonateDetails.Any(e => e.Id == id);
        }
    }
}
