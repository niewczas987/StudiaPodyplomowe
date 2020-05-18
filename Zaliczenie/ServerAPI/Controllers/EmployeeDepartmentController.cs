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
    public class EmployeeDepartmentController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public EmployeeDepartmentController(ProductsAndServicesContext context, ILogger<EmployeeDepartmentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę EmployeeDepartment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDepartment>>> GetEmployeeDepartment()
        {
            _logger.LogInformation("Get list of EmployeeDepartments");
            return await _context.EmployeeDepartment.ToListAsync();
        }

        /// <summary>
        /// Pobierz EmployeeDepartment o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDepartment>> GetEmployeeDepartment(long id)
        {
            var employeeDepartment = await _context.EmployeeDepartment.FindAsync(id);
            _logger.LogInformation("Get EmployeeDepartment with ID:"+id);
            if (employeeDepartment == null)
            {
                _logger.LogWarning("There's no EmployeeDepartment with ID:"+id);
                return NotFound();
            }

            return employeeDepartment;
        }

        // PUT: api/EmployeeDepartment/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDepartment(long id, EmployeeDepartment employeeDepartment)
        {
            if (id != employeeDepartment.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");
                return BadRequest();
            }

            _context.Entry(employeeDepartment).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to EmployeeDepartment with ID:"+id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDepartmentExists(id))
                {
                    _logger.LogInformation("There's no EmployeeDepartment with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogWarning("There's already EmployeeDepartment with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy EmployeeDepartment
        /// </summary>
        /// <param name="employeeDepartment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeDepartment>> PostEmployeeDepartment(EmployeeDepartment employeeDepartment)
        {
            _logger.LogInformation("Adding EmployeeDepartment");
            _context.EmployeeDepartment.Add(employeeDepartment);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");
            return CreatedAtAction("GetEmployeeDepartment", new { id = employeeDepartment.Id }, employeeDepartment);
        }

        /// <summary>
        /// Usuń z listy EmployeeDepartment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDepartment>> DeleteEmployeeDepartment(long id)
        {
            var employeeDepartment = await _context.EmployeeDepartment.FindAsync(id);
            if (employeeDepartment == null)
            {
                _logger.LogWarning("There's no EmployeeDepartment with ID:"+id);
                return NotFound();
            }
            try
            {
                _context.EmployeeDepartment.Remove(employeeDepartment);
            }
            catch
            {
                _logger.LogError("Record is already in use. You can't delete used records.");
            }
            
            
            await _context.SaveChangesAsync();
            _logger.LogInformation("EmployeeDepartment with ID:" + id + " was deleted.");
            return employeeDepartment;
        }

        private bool EmployeeDepartmentExists(long id)
        {
            return _context.EmployeeDepartment.Any(e => e.Id == id);
        }
    }
}
