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
    [Route("api/Route")]
    public class RouteController : Controller
    {
        private readonly GtfsContext _context;

        public RouteController(GtfsContext context)
        {
            _context = context;
        }

        // GET: api/Route
        [HttpGet]
        public IEnumerable<RouteItem> GetRoutes()
        {
            return _context.Routes;
        }

        // GET: api/Route/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeItem = await _context.Routes.SingleOrDefaultAsync(m => m.route_id == id);

            if (routeItem == null)
            {
                return NotFound();
            }

            return Ok(routeItem);
        }

        // PUT: api/Route/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteItem([FromRoute] string id, [FromBody] RouteItem routeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeItem.route_id)
            {
                return BadRequest();
            }

            _context.Entry(routeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteItemExists(id))
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

        // POST: api/Route
        [HttpPost]
        public async Task<IActionResult> PostRouteItem([FromBody] RouteItem routeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Routes.Add(routeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRouteItem", new { id = routeItem.route_id }, routeItem);
        }

        // DELETE: api/Route/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouteItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeItem = await _context.Routes.SingleOrDefaultAsync(m => m.route_id == id);
            if (routeItem == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(routeItem);
            await _context.SaveChangesAsync();

            return Ok(routeItem);
        }

        private bool RouteItemExists(string id)
        {
            return _context.Routes.Any(e => e.route_id == id);
        }
    }
}