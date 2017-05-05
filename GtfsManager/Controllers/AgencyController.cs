using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GTFSManagerApi.Models;

namespace GtfsManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Agency")]
    public class AgencyController : Controller
    {
        private readonly GtfsContext _context;

        public AgencyController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Agency
        [HttpGet]
        public IEnumerable<AgencyItem> GetAgencies()
        {
            return _context.Agencies;
        }

        // GET: api/Agency/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agencyItem = await _context.Agencies.SingleOrDefaultAsync(m => m.agency_id == id);

            if (agencyItem == null)
            {
                return NotFound();
            }

            return Ok(agencyItem);
        }

        // PUT: api/Agency/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgencyItem([FromRoute] string id, [FromBody] AgencyItem agencyItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agencyItem.agency_id)
            {
                return BadRequest();
            }

            _context.Entry(agencyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyItemExists(id))
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

        // POST: api/Agency
        [HttpPost]
        public async Task<IActionResult> PostAgencyItem([FromBody] AgencyItem agencyItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Agencies.Add(agencyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgencyItem", new { id = agencyItem.agency_id }, agencyItem);
        }

        // DELETE: api/Agency/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgencyItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agencyItem = await _context.Agencies.SingleOrDefaultAsync(m => m.agency_id == id);
            if (agencyItem == null)
            {
                return NotFound();
            }

            _context.Agencies.Remove(agencyItem);
            await _context.SaveChangesAsync();

            return Ok(agencyItem);
        }

        private bool AgencyItemExists(string id)
        {
            return _context.Agencies.Any(e => e.agency_id == id);
        }
    }
}