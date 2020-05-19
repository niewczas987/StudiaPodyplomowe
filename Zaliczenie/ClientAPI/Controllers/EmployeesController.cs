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
            _logger.LogInformation("Downloading details of Employees with ID:" + id);
            Employees employees = new Employees();
            var result = await _service.Client.GetAsync("/api/Employees/" + id);
            if(result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of Employees with ID:" + id);
                var downloadedEmployees = result.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<Employees>(downloadedEmployees);
                
            }
            else
            {
                _logger.LogInformation("Downloading details of Employees with ID:" + id + " ended with error. ErrorCode:"+result.StatusCode);
            }
            return View(employees);
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new Employees");
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
        public async Task<ActionResult> Create(Employees employees)
        {
            try
            {
                var postEmployees = JsonConvert.SerializeObject(employees);
                var result = await _service.Client.PostAsync("/api/Employees", new StringContent(postEmployees, Encoding.UTF8, "application/json"));
                if(result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employees posted succesfully");
                }
                else
                {
                    _logger.LogInformation("Employees not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Employees");
                return View();
            }
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new Employees");
            var result = await _service.Client.GetAsync("/api/EmployeePosition");
            var emplPosition = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfPositions = JsonConvert.DeserializeObject<List<EmployeePosition>>(emplPosition);
            result = await _service.Client.GetAsync("/api/EmployeeDepartment");
            var emplDepartment = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfDepartments = JsonConvert.DeserializeObject<List<EmployeeDepartment>>(emplDepartment);
            result = await _service.Client.GetAsync("/api/Employees/" + id);
            var employees = result.Content.ReadAsStringAsync().Result;
            ViewBag.Employee = JsonConvert.DeserializeObject<Employees>(employees);
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employees employees)
        {
            try
            {
                var postEmployees = JsonConvert.SerializeObject(employees);
                var result = await _service.Client.PutAsync("/api/Employees/"+id, new StringContent(postEmployees, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employees edited succesfully");
                }
                else
                {
                    _logger.LogInformation("Employees not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Employees");
                return View();
            }
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about Employees with ID:" + id);
            Employees employees = new Employees();
            var result = await _service.Client.GetAsync("/api/Employees/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about Employees with ID:" + id);
                var downloadedEmployees = result.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<Employees>(downloadedEmployees);

            }
            else
            {
                _logger.LogInformation("Deleting information about Employees with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Employees employees)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/Employees/"+id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employees deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("Employees not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting Employees");
                return View();
            }
        }
    }
}