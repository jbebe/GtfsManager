using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GTFSManagerApi.Models;

namespace GtfsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Stoptime")]
    public class StoptimeController : Controller
    {
        private readonly GtfsContext _context;

        public StoptimeController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Stoptime
        [HttpGet]
        public IEnumerable<StoptimeItem> GetStoptimes()
        {
            return _context.Stoptimes;
        }

        // GET: api/Stoptime/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoptimeItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stoptimeItem = await _context.Stoptimes.SingleOrDefaultAsync(m => m.primary_key == id);

            if (stoptimeItem == null)
            {
                return NotFound();
            }

            return Ok(stoptimeItem);
        }

        // PUT: api/Stoptime/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoptimeItem([FromRoute] string id, [FromBody] StoptimeItem stoptimeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stoptimeItem.primary_key)
            {
                return BadRequest();
            }

            _context.Entry(stoptimeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoptimeItemExists(id))
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

        // POST: api/Stoptime
        [HttpPost]
        public async Task<IActionResult> PostStoptimeItem([FromBody] StoptimeItem stoptimeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stoptimes.Add(stoptimeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoptimeItem", new { id = stoptimeItem.primary_key }, stoptimeItem);
        }

        // DELETE: api/Stoptime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoptimeItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stoptimeItem = await _context.Stoptimes.SingleOrDefaultAsync(m => m.primary_key == id);
            if (stoptimeItem == null)
            {
                return NotFound();
            }

            _context.Stoptimes.Remove(stoptimeItem);
            await _context.SaveChangesAsync();

            return Ok(stoptimeItem);
        }

        private bool StoptimeItemExists(string id)
        {
            return _context.Stoptimes.Any(e => e.primary_key == id);
        }
    }
}