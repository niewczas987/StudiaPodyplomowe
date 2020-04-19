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
    public class AdresController : ControllerBase
    {
        private readonly BazaFilmowaContext _context;

        public AdresController(BazaFilmowaContext context)
        {
            _context = context;
        }

        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adres>>> GetAdres()
        {
            return await _context.Adres.ToListAsync();
        }

        // GET: api/Adres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adres>> GetAdres(long id)
        {
            var adres = await _context.Adres.FindAsync(id);

            if (adres == null)
            {
                return NotFound();
            }

            return adres;
        }

        // PUT: api/Adres/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdres(long id, Adres adres)
        {
            if (id != adres.Id)
            {
                return BadRequest();
            }

            _context.Entry(adres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresExists(id))
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

        // POST: api/Adres
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Adres>> PostAdres(Adres adres)
        {
            _context.Adres.Add(adres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdres", new { id = adres.Id }, adres);
        }

        // DELETE: api/Adres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Adres>> DeleteAdres(long id)
        {
            var adres = await _context.Adres.FindAsync(id);
            if (adres == null)
            {
                return NotFound();
            }

            _context.Adres.Remove(adres);
            await _context.SaveChangesAsync();

            return adres;
        }

        private bool AdresExists(long id)
        {
            return _context.Adres.Any(e => e.Id == id);
        }
    }
}
