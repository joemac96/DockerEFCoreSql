using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagazinesController : ControllerBase
    {
        private readonly MagazinesContext _context;

        public MagazinesController(MagazinesContext context)
        {
            _context = context;
        }

        private bool MagazineExists(int id)
        {
            return (_context.Magazine?.Any(e => e.MagazineId == id)).GetValueOrDefault();
        }

        // DELETE: api/Magazines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagazine(int id)
        {
            if(_context.Magazine == null)
            {
                return NotFound();
            }
            var magazine = await _context.Magazine.FindAsync(id);
            if(magazine == null)
            {
                return NotFound();
            }

            _context.Magazine.Remove(magazine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Magazines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Magazine>>> GetMagazine()
        {
            if(_context.Magazine == null)
            {
                return NotFound();
            }
            return await _context.Magazine.ToListAsync();
        }

        // GET: api/Magazines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Magazine>> GetMagazine(int id)
        {
            if(_context.Magazine == null)
            {
                return NotFound();
            }
            var magazine = await _context.Magazine.FindAsync(id);

            if(magazine == null)
            {
                return NotFound();
            }

            return magazine;
        }

        // POST: api/Magazines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Magazine>> PostMagazine(Magazine magazine)
        {
            if(_context.Magazine == null)
            {
                return Problem("Entity set 'MagazinesContext.Magazine'  is null.");
            }
            _context.Magazine.Add(magazine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagazine", new { id = magazine.MagazineId }, magazine);
        }

        // PUT: api/Magazines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagazine(int id, Magazine magazine)
        {
            if(id != magazine.MagazineId)
            {
                return BadRequest();
            }

            _context.Entry(magazine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!MagazineExists(id))
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
    }
}
