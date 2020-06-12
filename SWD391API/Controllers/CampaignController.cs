using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWD391API.Models;
using SWD391API.Paging;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace SWD391API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly SWD391Context _context;

        public CampaignController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/Campaigns
        [Microsoft.AspNetCore.Mvc.Route("[action]")]
        [HttpGet]
        public  IEnumerable<Campaign> Campaigns([FromQuery]PagingParameterModel pagingParameterModel)
        {
            var campaigns = _context.Campaign.AsQueryable(); 
            int count = campaigns.Count();

            int CurrentPage = pagingParameterModel.pageNumber;
            int PageSize = pagingParameterModel.pageSize;

            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
 
            var items = campaigns.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }

        // GET: campaigns/CampaignsNewest/5
        [Microsoft.AspNetCore.Mvc.Route("[action]/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult> CampaignsNewest(int id)
        {
            if (id == -1)
            {
                var campaigns = _context.Campaign.OrderByDescending(c => c.StartDate)
                                 .Include(s => s.Author)
                                 .Select(s => new
                                 {
                                     firstName = s.Author.FirstName,
                                     lastName = s.Author.LastName,
                                     cammpaignId = s.Id,
                                     campaignName = s.CampaignName,
                                     careless = s.Popular,
                                     startDate = s.StartDate,
                                     endDate = s.EndDate,
                                     description = s.Description
                                 })
                                .ToList();
                return Ok(campaigns);
            }
            else
            {
                var campaigns = _context.Campaign.OrderByDescending(c => c.StartDate)
                                .Take(id)
                                 .Include(s => s.Author)
                                 .Select(s => new
                                 {
                                     firstName = s.Author.FirstName,
                                     lastName = s.Author.LastName,
                                     cammpaignId = s.Id,
                                     campaignName = s.CampaignName,
                                     careless = s.Popular,
                                     startDate = s.StartDate,
                                     endDate = s.EndDate,
                                     description = s.Description
                                 })
                                .ToList();
                return Ok(campaigns);
            }

        }

        [Microsoft.AspNetCore.Mvc.Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> CampaignsOldest(int id)
        {
            if (id == -1)
            {
                var campaigns = _context.Campaign.OrderByDescending(c => c.StartDate)
                                 .Include(s => s.Author)
                                 .Select(s => new
                                 {
                                     firstName = s.Author.FirstName,
                                     lastName = s.Author.LastName,
                                     cammpaignId = s.Id,
                                     campaignName = s.CampaignName,
                                     careless = s.Popular,
                                     startDate = s.StartDate,
                                     endDate = s.EndDate,
                                     description = s.Description
                                 })
                                .ToList();
                return Ok(campaigns);
            }
            else
            {
                var campaigns = _context.Campaign.OrderByDescending(c => c.StartDate)
                                .Take(id)
                                 .Include(s => s.Author)
                                 .Select(s => new
                                 {
                                     firstName = s.Author.FirstName,
                                     lastName = s.Author.LastName,
                                     cammpaignId = s.Id,
                                     campaignName = s.CampaignName,
                                     careless = s.Popular,
                                     startDate = s.StartDate,
                                     endDate = s.EndDate,
                                     description = s.Description
                                 })
                                .ToList();
                return Ok(campaigns);
            }

        }


        // PUT: api/Campaigns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> PutCampaigns(int id, Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return BadRequest();
            }

            _context.Entry(campaign).State = EntityState.Modified;

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
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaigns(int id)
        {
            var campaigns = await _context.Campaign.FindAsync(id);
            if (campaigns == null)
            {
                return NotFound();
            }

            _context.Campaign.Remove(campaigns);
            await _context.SaveChangesAsync();

            return campaigns;
        }

        private bool CampaignsExists(int id)
        {
            return _context.Campaign.Any(e => e.Id == id);
        }
    }
}
