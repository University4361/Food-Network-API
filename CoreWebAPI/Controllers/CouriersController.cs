using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPI.EF;
using CoreWebAPI.Models;

namespace CoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Couriers")]
    public class CouriersController : Controller
    {
        private readonly DataContext _context;

        public CouriersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Couriers
        [HttpGet]
        public IEnumerable<Courier> GetCourier()
        {
            return _context.Courier;
        }

        // GET: api/Couriers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courier = await _context.Courier.SingleOrDefaultAsync(m => m.ID == id);

            if (courier == null)
            {
                return NotFound();
            }

            return Ok(courier);
        }

        // PUT: api/Couriers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourier([FromRoute] int id, [FromBody] Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courier.ID)
            {
                return BadRequest();
            }

            _context.Entry(courier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierExists(id))
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

        // POST: api/Couriers
        [HttpPost]
        public async Task<IActionResult> PostCourier([FromBody] Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Courier.Add(courier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourier", new { id = courier.ID }, courier);
        }

        // DELETE: api/Couriers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courier = await _context.Courier.SingleOrDefaultAsync(m => m.ID == id);
            if (courier == null)
            {
                return NotFound();
            }

            _context.Courier.Remove(courier);
            await _context.SaveChangesAsync();

            return Ok(courier);
        }

        private bool CourierExists(int id)
        {
            return _context.Courier.Any(e => e.ID == id);
        }
    }
}