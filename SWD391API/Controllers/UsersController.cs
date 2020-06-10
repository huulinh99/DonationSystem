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
    public class UsersController : ControllerBase
    {
        private readonly SWD391Context _context;

        public UsersController(SWD391Context context)
        {
            _context = context;
        }

        // GET: api/Users
        [Route("[action]")]
        [HttpGet]
        public ActionResult UserMostFavourite()
        {

            var user = _context.Users
                                     .FromSqlRaw("Select COUNT(c.UserId) as totalCampaigns, (SELECT Count(ca.CampaignId) from Carelesses ca where ca.UserId=(select top 1 UserId From Carelesses Group by UserId Order by COUNT(CampaignId) Desc)) as totalLikes from Users u join Campaigns c on u.UserId= c.UserId where u.UserId=( select top 1 UserId From Carelesses Group by UserId Order by COUNT(CampaignId) Desc)")
                                     .ToList();
            
            return Ok(user);
        }

        // GET: api/Users/5
        [Route("[action]/{userId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers(string userId)
        {
            var users =  _context.Users.Fr        
            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(string id, Users users)
        {
            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(string id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
