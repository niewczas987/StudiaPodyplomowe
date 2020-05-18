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
    public class EmployeeDepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;
        public EmployeeDepartmentController(IAPIService service, ILogger<EmployeeDepartmentController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: EmployeeDepartments
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of EmployeeDepartments");
            List<EmployeeDepartment> departments = new List<EmployeeDepartment>();
            var result = await _service.Client.GetAsync("/api/EmployeeDepartment");
            if (result.IsSuccessStatusCode)
            {
                var downloadedDepartments = result.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<EmployeeDepartment>>(downloadedDepartments);
                _logger.LogInformation("EmployeeDepartments downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading EmployeeDepartments. ErrorStatusCode:" + result.StatusCode);
            }
            return View(departments);

        }

        // GET: EmployeeDepartments/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of EmployeeDepartment with ID:" + id);
            EmployeeDepartment departments = new EmployeeDepartment();
            var result = await _service.Client.GetAsync("/api/EmployeeDepartment/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of EmployeeDepartment with ID:" + id);
                var downloadedDepartments = result.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<EmployeeDepartment>(downloadedDepartments);

            }
            else
            {
                _logger.LogInformation("Downloading details of EmployeeDepartment with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(departments);
        }

        // GET: EmployeeDepartment/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new EmployeeDepartment");
            return View();
        }

        // POST: EmployeeDepartment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeDepartment department)
        {
            try
            {
                var postDepartment = JsonConvert.SerializeObject(department);
                var result = await _service.Client.PostAsync("/api/EmployeeDepartment", new StringContent(postDepartment, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeeDepartment posted succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeeDepartment not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeeDepartment");
                return View();
            }
        }

        // GET: EmployeeDepartment/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new Employee");
            var result = await _service.Client.GetAsync("/api/EmployeeDepartment/" + id);
            var department = result.Content.ReadAsStringAsync().Result;
            ViewBag.EmployeeDepartment = JsonConvert.DeserializeObject<EmployeeDepartment>(department);
            return View();
        }

        // POST: EmployeeDepartment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeDepartment department)
        {
            try
            {
                var postDepartment = JsonConvert.SerializeObject(department);
                var result = await _service.Client.PutAsync("/api/EmployeeDepartment/" + id, new StringContent(postDepartment, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeeDepartment edited succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeeDepartment not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeeDepartment");
                return View();
            }
        }

        // GET: EmployeeDepartment/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about EmployeeDepartment with ID:" + id);
            EmployeeDepartment department = new EmployeeDepartment();
            var result = await _service.Client.GetAsync("/api/EmployeeDepartment/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about EmployeeDepartment with ID:" + id);
                var downloadedDepartment = result.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<EmployeeDepartment>(downloadedDepartment);

            }
            else
            {
                _logger.LogInformation("Deleting information about EmployeeDepartment with  ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(department);
        }

        // POST: EmployeeDepartment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EmployeeDepartment department)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/EmployeeDepartment/" + id);
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