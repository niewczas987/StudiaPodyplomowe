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
    public class KlientController : ControllerBase
    {
        private readonly BazaFilmowaContext _context;

        public KlientController(BazaFilmowaContext context)
        {
            _context = context;
        }

        // GET: api/Klient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klient>>> GetKlient()
        {
            return await _context.Klient.ToListAsync();
        }

        // GET: api/Klient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Klient>> GetKlient(long id)
        {
            var klient = await _context.Klient.FindAsync(id);

            if (klient == null)
            {
                return NotFound();
            }

            return klient;
        }

        // PUT: api/Klient/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKlient(long id, Klient klient)
        {
            if (id != klient.Id)
            {
                return BadRequest();
            }

            _context.Entry(klient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlientExists(id))
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

        // POST: api/Klient
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Klient>> PostKlient(Klient klient)
        {
            _context.Klient.Add(klient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKlient", new { id = klient.Id }, klient);
        }

        // DELETE: api/Klient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Klient>> DeleteKlient(long id)
        {
            var klient = await _context.Klient.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }

            _context.Klient.Remove(klient);
            await _context.SaveChangesAsync();

            return klient;
        }

        private bool KlientExists(long id)
        {
            return _context.Klient.Any(e => e.Id == id);
        }
    }
}
