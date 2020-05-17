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
    public class EmployeeRiseController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public EmployeeRiseController(ProductsAndServicesContext context, ILogger<EmployeeRiseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę EmployeeRise
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeRise>>> GetEmployeeRise()
        {
            _logger.LogInformation("Get list of EmployeeRise");
            List<EmployeeRise> employeeRises = new List<EmployeeRise>();
            employeeRises = await _context.EmployeeRise.ToListAsync();
            foreach (EmployeeRise rise in employeeRises)
            {
                rise.IdemployeeNavigation = await _context.Employees.FindAsync(rise.Idemployee);
                rise.IdemployeeNavigation.PositionNavigation = await _context.EmployeePosition.FindAsync(rise.Idemployee);
                rise.IdemployeeNavigation.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(rise.Idemployee);
            }
            return employeeRises;
        }

        /// <summary>
        /// Pobierz EmployeeRise o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeRise>> GetEmployeeRise(long id)
        {
            var employeeRise = await _context.EmployeeRise.FindAsync(id);
            _logger.LogInformation("Get EmployeeRise with ID:" + id);
            if (employeeRise == null)
            {
                _logger.LogWarning("There's no EmployeeRise with ID:" + id);
                return NotFound();
            }
            employeeRise.IdemployeeNavigation = await _context.Employees.FindAsync(employeeRise.Idemployee);
            employeeRise.IdemployeeNavigation.PositionNavigation = await _context.EmployeePosition.FindAsync(employeeRise.Idemployee);
            employeeRise.IdemployeeNavigation.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(employeeRise.Idemployee);
            return employeeRise;
        }

        // PUT: api/EmployeeRises/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeRise(long id, EmployeeRise employeeRise)
        {
            if (id != employeeRise.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");
                return BadRequest();
            }

            _context.Entry(employeeRise).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to EmployeeRise with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeRiseExists(id))
                {
                    _logger.LogInformation("There's no EmployeeRise with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("There's already EmployeeRise with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy EmployeeRise
        /// </summary>
        /// <param name="employeeRise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeRise>> PostEmployeeRise(EmployeeRise employeeRise)
        {
            _context.EmployeeRise.Add(employeeRise);
            _logger.LogInformation("Adding EmployeeRise");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");
            return CreatedAtAction("GetEmployeeRise", new { id = employeeRise.Id }, employeeRise);
        }

        /// <summary>
        /// Usuń z listy EmployeeRise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeRise>> DeleteEmployeeRise(long id)
        {
            var employeeRise = await _context.EmployeeRise.FindAsync(id);
            if (employeeRise == null)
            {
                _logger.LogWarning("There's no EmployeeRise with ID:" + id);
                return NotFound();
            }

            _context.EmployeeRise.Remove(employeeRise);
            await _context.SaveChangesAsync();
            _logger.LogInformation("EmployeeRise with ID:" + id + " was deleted.");
            return employeeRise;
        }

        private bool EmployeeRiseExists(long id)
        {
            return _context.EmployeeRise.Any(e => e.Id == id);
        }
    }
}
