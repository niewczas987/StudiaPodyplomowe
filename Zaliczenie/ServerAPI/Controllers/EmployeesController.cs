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
    public class EmployeesController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public EmployeesController(ProductsAndServicesContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            _logger.LogInformation("Get list of EmployeeDepartments");
            List<Employees> employees = new List<Employees>();
            employees = await _context.Employees.ToListAsync();
            foreach(Employees employee in employees)
            {
                employee.PositionNavigation = await _context.EmployeePosition.FindAsync(employee.Position);
                employee.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(employee.Department);
            }
            return employees;

        }

        /// <summary>
        /// Pobierz Employee o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(long id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _logger.LogInformation("Get Employee with ID:" + id);
            if (employees == null)
            {
                _logger.LogWarning("There's no Employee with ID:" + id);
                return NotFound();
            }
            employees.PositionNavigation = await _context.EmployeePosition.FindAsync(employees.Position);
            employees.DepartmentNavigation = await _context.EmployeeDepartment.FindAsync(employees.Department);
            

            return employees;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees(long id, Employees employees)
        {
            if (id != employees.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");
                return BadRequest();
            }

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to Employee with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    _logger.LogInformation("There's no Employee with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("There's already Employee with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy Employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(Employees employees)
        {
            _context.Employees.Add(employees);
            _logger.LogInformation("Adding Employee");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");

            return CreatedAtAction("GetEmployees", new { id = employees.Id }, employees);
        }

        /// <summary>
        /// Usuń z listy Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> DeleteEmployees(long id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                _logger.LogWarning("There's no Employee with ID:" + id);
                return NotFound();
            }

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee with ID:" + id + " was deleted.");
            return employees;
        }

        private bool EmployeesExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
