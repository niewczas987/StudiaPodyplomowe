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
    public class EmployeeGradeController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;
        public EmployeeGradeController(ProductsAndServicesContext context, ILogger<EmployeeGradeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę EmployeeGrade
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGrade>>> GetEmployeeGrade()
        {
            _logger.LogInformation("Get list of EmployeeGrades");
            List<EmployeeGrade> employeeGrades = new List<EmployeeGrade>();
            employeeGrades = await _context.EmployeeGrade.ToListAsync();
            foreach (EmployeeGrade grade in employeeGrades)
            {
                grade.IdemployeeNavigation = await _context.Employees.FindAsync(grade.Idemployee);
                grade.IdemployeeNavigation.PositionNavigation = await _context.EmployeePosition.FindAsync(grade.Idemployee);
                grade.IdemployeeNavigation.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(grade.Idemployee);
            }
            return employeeGrades;
        }

        /// <summary>
        /// Pobierz EmployeeGrade o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGrade>> GetEmployeeGrade(long id)
        {
            var employeeGrade = await _context.EmployeeGrade.FindAsync(id);

            if (employeeGrade == null)
            {
                _logger.LogWarning("There's no EmployeeGrade with ID:"+id);
                return NotFound();
            }
            employeeGrade.IdemployeeNavigation = await _context.Employees.FindAsync(employeeGrade.Idemployee);
            employeeGrade.IdemployeeNavigation.PositionNavigation = await _context.EmployeePosition.FindAsync(employeeGrade.Idemployee);
            employeeGrade.IdemployeeNavigation.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(employeeGrade.Idemployee);
            return employeeGrade;
        }

        // PUT: api/EmployeeGrade/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeGrade(long id, EmployeeGrade employeeGrade)
        {
            if (id != employeeGrade.Id)
            {
                _logger.LogWarning("Incorrect id on put request");
                return BadRequest();
            }

            _context.Entry(employeeGrade).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to EmployeeGrade with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeGradeExists(id))
                {
                    _logger.LogInformation("There's no EmployeeDepartment with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("There's already EmployeeDepartment with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy EmployeeGrade
        /// </summary>
        /// <param name="employeeGrade"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeGrade>> PostEmployeeGrade(EmployeeGrade employeeGrade)
        {
            _context.EmployeeGrade.Add(employeeGrade);
            _logger.LogInformation("Adding EmployeeGrade");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");
            return CreatedAtAction("GetEmployeeGrade", new { id = employeeGrade.Id }, employeeGrade);
        }

        /// <summary>
        /// Usuń z listy EmployeeGrade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeGrade>> DeleteEmployeeGrade(long id)
        {
            var employeeGrade = await _context.EmployeeGrade.FindAsync(id);
            if (employeeGrade == null)
            {
                _logger.LogWarning("There's no EmployeeGrade with ID:" + id);
                return NotFound();
            }

            _context.EmployeeGrade.Remove(employeeGrade);
            await _context.SaveChangesAsync();
            _logger.LogInformation("EmployeeDepartment with ID:" + id + " was deleted.");
            return employeeGrade;
        }

        private bool EmployeeGradeExists(long id)
        {
            return _context.EmployeeGrade.Any(e => e.Id == id);
        }
    }
}
