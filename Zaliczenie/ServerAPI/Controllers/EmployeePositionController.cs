using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerAPI.Models;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePositionController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public EmployeePositionController(ProductsAndServicesContext context, ILogger<EmployeePositionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę EmployeePosition
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetEmployeePosition()
        {
            _logger.LogInformation("Get list of EmployeePosition");
            return await _context.EmployeePosition.ToListAsync();
        }

        /// <summary>
        /// Pobierz EmployeePosition o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePosition>> GetEmployeePosition(long id)
        {
            var employeePosition = await _context.EmployeePosition.FindAsync(id);

            if (employeePosition == null)
            {
                _logger.LogWarning("There's no EmployeePosition with ID:" + id);
                return NotFound();
            }

            return employeePosition;
        }

        // PUT: api/EmployeePosition/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePosition(long id, EmployeePosition employeePosition)
        {
            if (id != employeePosition.Id)
            {
                _logger.LogWarning("Incorrect id on put request");
                return BadRequest();
            }

            _context.Entry(employeePosition).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to EmployeePosition with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePositionExists(id))
                {
                    _logger.LogInformation("There's no EmployeePosition with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("There's already EmployeePosition with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy EmployeePosition
        /// </summary>
        /// <param name="employeePosition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeePosition>> PostEmployeePosition(EmployeePosition employeePosition)
        {
            _context.EmployeePosition.Add(employeePosition);
            _logger.LogInformation("Adding EmployeeGrade");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");
            return CreatedAtAction("GetEmployeePosition", new { id = employeePosition.Id }, employeePosition);
        }

        /// <summary>
        /// Usuń z listy EmployeePosition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeePosition>> DeleteEmployeePosition(long id)
        {
            var employeePosition = await _context.EmployeePosition.FindAsync(id);
            if (employeePosition == null)
            {
                _logger.LogWarning("There's no EmployeePosition with ID:" + id);
                return NotFound();
            }

            _context.EmployeePosition.Remove(employeePosition);
            await _context.SaveChangesAsync();
            _logger.LogInformation("EmployeePosition with ID:" + id + " was deleted.");
            return employeePosition;
        }

        private bool EmployeePositionExists(long id)
        {
            return _context.EmployeePosition.Any(e => e.Id == id);
        }
    }
}
