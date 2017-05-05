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
    [Route("api/Stop")]
    public class StopController : Controller
    {
        private readonly GtfsContext _context;

        public StopController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Stop
        [HttpGet]
        public IEnumerable<StopItem> GetStops()
        {
            return _context.Stops;
        }

        // GET: api/Stop/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStopItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stopItem = await _context.Stops.SingleOrDefaultAsync(m => m.stop_id == id);

            if (stopItem == null)
            {
                return NotFound();
            }

            return Ok(stopItem);
        }

        // PUT: api/Stop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStopItem([FromRoute] string id, [FromBody] StopItem stopItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stopItem.stop_id)
            {
                return BadRequest();
            }

            _context.Entry(stopItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopItemExists(id))
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

        // POST: api/Stop
        [HttpPost]
        public async Task<IActionResult> PostStopItem([FromBody] StopItem stopItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stops.Add(stopItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStopItem", new { id = stopItem.stop_id }, stopItem);
        }

        // DELETE: api/Stop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStopItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stopItem = await _context.Stops.SingleOrDefaultAsync(m => m.stop_id == id);
            if (stopItem == null)
            {
                return NotFound();
            }

            _context.Stops.Remove(stopItem);
            await _context.SaveChangesAsync();

            return Ok(stopItem);
        }

        private bool StopItemExists(string id)
        {
            return _context.Stops.Any(e => e.stop_id == id);
        }
    }
}