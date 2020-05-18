using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.Data;
using ClientAPI.Models;
using ClientAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClientAPI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public EmployeesController(IAPIService service, ILogger<EmployeesController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of Employees");
            List<Employees> employees = new List<Employees>();
            var result = await _service.Client.GetAsync("/api/Employees");
            if (result.IsSuccessStatusCode)
            {
                var downloadedEmployees = result.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employees>>(downloadedEmployees);
                _logger.LogInformation("Employees downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading Employees. ErrorStatusCode:" + result.StatusCode);
            }
            return View(employees);
            
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of user with ID:" + id);
            Employees employee = new Employees();
            var result = await _service.Client.GetAsync("/api/Employees/" + id);
            if(result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of user with ID:" + id);
                var downloadedEmployee = result.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employees>(downloadedEmployee);
                
            }
            else
            {
                _logger.LogInformation("Downloading details of user with ID:" + id + " ended with error. ErrorCode:"+result.StatusCode);
            }
            return View(employee);
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new Employee");
            var result = await _service.Client.GetAsync("/api/EmployeePosition");
            var emplPosition = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfPositions = JsonConvert.DeserializeObject<List<EmployeePosition>>(emplPosition);
            result = await _service.Client.GetAsync("/api/EmployeeDepartment");
            var emplDepartment = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfDepartments = JsonConvert.DeserializeObject<List<EmployeeDepartment>>(emplDepartment);

            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employees employee)
        {
            try
            {
                var postEmployee = JsonConvert.SerializeObject(employee);
                var result = await _service.Client.PostAsync("/api/Employees", new StringContent(postEmployee, Encoding.UTF8, "application/json"));
                if(result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employee posted succesfully");
                }
                else
                {
                    _logger.LogInformation("Employee not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Employee");
                return View();
            }
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new Employee");
            var result = await _service.Client.GetAsync("/api/EmployeePosition");
            var emplPosition = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfPositions = JsonConvert.DeserializeObject<List<EmployeePosition>>(emplPosition);
            result = await _service.Client.GetAsync("/api/EmployeeDepartment");
            var emplDepartment = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfDepartments = JsonConvert.DeserializeObject<List<EmployeeDepartment>>(emplDepartment);
            result = await _service.Client.GetAsync("/api/Employees/" + id);
            var employee = result.Content.ReadAsStringAsync().Result;
            ViewBag.Employee = JsonConvert.DeserializeObject<Employees>(employee);
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employees employee)
        {
            try
            {
                var postEmployee = JsonConvert.SerializeObject(employee);
                var result = await _service.Client.PutAsync("/api/Employees/"+id, new StringContent(postEmployee, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employee edited succesfully");
                }
                else
                {
                    _logger.LogInformation("Employee not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Employee");
                return View();
            }
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about user with ID:" + id);
            Employees employee = new Employees();
            var result = await _service.Client.GetAsync("/api/Employees/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about user with ID:" + id);
                var downloadedEmployee = result.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employees>(downloadedEmployee);

            }
            else
            {
                _logger.LogInformation("Deleting information about user with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Employees employee)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/Employees/"+id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employee deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("Employee not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting Employee");
                return View();
            }
        }
    }
}