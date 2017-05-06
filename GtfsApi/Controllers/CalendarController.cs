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
    [Route("api/Calendar")]
    public class CalendarController : Controller
    {
        private readonly GtfsContext _context;

        public CalendarController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Calendar
        [HttpGet]
        public IEnumerable<CalendarItem> GetCalendar()
        {
            return _context.Calendar;
        }

        // GET: api/Calendar/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCalendarItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calendarItem = await _context.Calendar.SingleOrDefaultAsync(m => m.primary_key == id);

            if (calendarItem == null)
            {
                return NotFound();
            }

            return Ok(calendarItem);
        }

        // PUT: api/Calendar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarItem([FromRoute] string id, [FromBody] CalendarItem calendarItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calendarItem.primary_key)
            {
                return BadRequest();
            }

            _context.Entry(calendarItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarItemExists(id))
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

        // POST: api/Calendar
        [HttpPost]
        public async Task<IActionResult> PostCalendarItem([FromBody] CalendarItem calendarItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Calendar.Add(calendarItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendarItem", new { id = calendarItem.primary_key }, calendarItem);
        }

        // DELETE: api/Calendar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calendarItem = await _context.Calendar.SingleOrDefaultAsync(m => m.primary_key == id);
            if (calendarItem == null)
            {
                return NotFound();
            }

            _context.Calendar.Remove(calendarItem);
            await _context.SaveChangesAsync();

            return Ok(calendarItem);
        }

        private bool CalendarItemExists(string id)
        {
            return _context.Calendar.Any(e => e.primary_key == id);
        }
    }
}