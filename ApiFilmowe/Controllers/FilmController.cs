using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFilmowe.Modele;
using Microsoft.Extensions.Logging;

namespace ApiFilmowe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly BazaFilmowaContext _context;
        private readonly ILogger _logger;

        public FilmController(BazaFilmowaContext context, ILogger<FilmController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Film
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilm()
        {
            return await _context.Film.ToListAsync();
        }

        // GET: api/Film/5
        /// <summary>
        /// Pobierz film o danym id
        /// </summary>
        /// <param name="id">ID filmu</param>
        /// <returns>Dane filmu w postaci klasy Film</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(long id)
        {
            _logger.LogInformation($"Wywołanie zapytania o film o ID :{id}");
            var film = await _context.Film.FindAsync(id);

            if (film == null)
            {
                _logger.LogWarning("Nie ma filmu o ID: " + id);
                return NotFound();
            }

            return film;
        }

        // PUT: api/Film/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(long id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Film
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteFilm(long id)
        {
            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Film.Remove(film);
            await _context.SaveChangesAsync();

            return film;
        }

        private bool FilmExists(long id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
