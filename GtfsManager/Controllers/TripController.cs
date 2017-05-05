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
    [Route("api/Trip")]
    public class TripController : Controller
    {
        private readonly GtfsContext _context;

        public TripController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Trip
        [HttpGet]
        public IEnumerable<TripItem> GetTrips()
        {
            return _context.Trips;
        }

        // GET: api/Trip/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripItem = await _context.Trips.SingleOrDefaultAsync(m => m.trip_id == id);

            if (tripItem == null)
            {
                return NotFound();
            }

            return Ok(tripItem);
        }

        // PUT: api/Trip/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripItem([FromRoute] string id, [FromBody] TripItem tripItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripItem.trip_id)
            {
                return BadRequest();
            }

            _context.Entry(tripItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripItemExists(id))
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

        // POST: api/Trip
        [HttpPost]
        public async Task<IActionResult> PostTripItem([FromBody] TripItem tripItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(tripItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripItem", new { id = tripItem.trip_id }, tripItem);
        }

        // DELETE: api/Trip/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripItem = await _context.Trips.SingleOrDefaultAsync(m => m.trip_id == id);
            if (tripItem == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(tripItem);
            await _context.SaveChangesAsync();

            return Ok(tripItem);
        }

        private bool TripItemExists(string id)
        {
            return _context.Trips.Any(e => e.trip_id == id);
        }
    }
}