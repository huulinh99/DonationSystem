using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD391API.Models;

namespace SWD391API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly SWD391Context _context;

        public CampaignsController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult> GetCampaigns()
        {
            var campaigns = _context.Campaigns.ToList();
            return Ok(new { results = campaigns });
        }

        // GET: campaigns/CampaignsNewest/5
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> CampaignsNewest(int id)
        {
            if (id == -1) 
            {
                var campaigns = _context.Campaigns                       
                                .OrderByDescending(x => x.StartDate)
                                .ToList();
                return Ok(new { results = campaigns });
            }
            else
            {
                var campaigns = _context.Campaigns.OrderByDescending(c => c.StartDate)
                                .Take(id)
                                 .Include(s => s.User)
                                 .Select(s => new {
                                     firstName = s.User.FirstName,
                                     lastName = s.User.LastName,
                                     cammpaignId = s.CampaignId,
                                     campaignName = s.CampaignName,
                                     careless = s.Carelesses.Count,
                                     startDate = s.StartDate,
                                     endDate = s.EndDate
                                 })
                                .ToList();
                return Ok(campaigns);
            }
            
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> CampaignsOldest(int id)
        {
            if (id == -1)
            {
                var campaigns = _context.Campaigns
                                .OrderByDescending(x => x.EndDate)
                                .ToList();
                return Ok(new { results = campaigns });
            }
            else
            {
                var campaigns = _context.Campaigns
                                .OrderByDescending(c => c.EndDate)
                                .Take(id)
                                .ToList();
                return Ok(new { results = campaigns });
            }

        }


        // PUT: api/Campaigns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaigns(int id, Campaigns campaigns)
        {
            if (id != campaigns.CampaignId)
            {
                return BadRequest();
            }

            _context.Entry(campaigns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignsExists(id))
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

        // POST: api/Campaigns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaigns>> DeleteCampaigns(int id)
        {
            var campaigns = await _context.Campaigns.FindAsync(id);
            if (campaigns == null)
            {
                return NotFound();
            }

            _context.Campaigns.Remove(campaigns);
            await _context.SaveChangesAsync();

            return campaigns;
        }

        private bool CampaignsExists(int id)
        {
            return _context.Campaigns.Any(e => e.CampaignId == id);
        }
    }
}
