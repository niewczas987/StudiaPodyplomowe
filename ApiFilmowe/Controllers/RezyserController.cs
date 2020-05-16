using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFilmowe.Modele;

namespace ApiFilmowe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezyserController : ControllerBase
    {
        private readonly BazaFilmowaContext _context;

        public RezyserController(BazaFilmowaContext context)
        {
            _context = context;
        }

        // GET: api/Rezyser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezyser>>> GetRezyser()
        {
            return await _context.Rezyser.ToListAsync();
        }

        // GET: api/Rezyser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rezyser>> GetRezyser(long id)
        {
            var rezyser = await _context.Rezyser.FindAsync(id);

            if (rezyser == null)
            {
                return NotFound();
            }

            return rezyser;
        }

        // PUT: api/Rezysers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezyser(long id, Rezyser rezyser)
        {
            if (id != rezyser.Id)
            {
                return BadRequest();
            }

            _context.Entry(rezyser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezyserExists(id))
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

        // POST: api/Rezysers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rezyser>> PostRezyser(Rezyser rezyser)
        {
            _context.Rezyser.Add(rezyser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRezyser", new { id = rezyser.Id }, rezyser);
        }

        // DELETE: api/Rezysers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rezyser>> DeleteRezyser(long id)
        {
            var rezyser = await _context.Rezyser.FindAsync(id);
            if (rezyser == null)
            {
                return NotFound();
            }

            _context.Rezyser.Remove(rezyser);
            await _context.SaveChangesAsync();

            return rezyser;
        }

        private bool RezyserExists(long id)
        {
            return _context.Rezyser.Any(e => e.Id == id);
        }
    }
}
