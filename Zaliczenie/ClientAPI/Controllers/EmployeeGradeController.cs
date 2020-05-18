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
    public class EmployeeGradeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public EmployeeGradeController(IAPIService service, ILogger<EmployeeGradeController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: EmployeeGrade
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of EmployeeGrade");
            List<EmployeeGrade> employeeGrade = new List<EmployeeGrade>();
            var result = await _service.Client.GetAsync("/api/EmployeeGrade");
            if (result.IsSuccessStatusCode)
            {
                var downloadedEmployeeGrade = result.Content.ReadAsStringAsync().Result;
                employeeGrade = JsonConvert.DeserializeObject<List<EmployeeGrade>>(downloadedEmployeeGrade);
                _logger.LogInformation("EmployeeGrade downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading EmployeeGrade. ErrorStatusCode:" + result.StatusCode);
            }
            return View(employeeGrade);

        }

        // GET: EmployeeGrade/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of EmployeeGrade with ID:" + id);
            EmployeeGrade employeeGrade = new EmployeeGrade();
            var result = await _service.Client.GetAsync("/api/EmployeeGrade/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of EmployeeGrade with ID:" + id);
                var downloadedEmployeeGrade = result.Content.ReadAsStringAsync().Result;
                employeeGrade = JsonConvert.DeserializeObject<EmployeeGrade>(downloadedEmployeeGrade);

            }
            else
            {
                _logger.LogInformation("Downloading details of user with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            result = await _service.Client.GetAsync("/api/EmployeeDepartment/" + id);
            var employee = result.Content.ReadAsStringAsync().Result;
            ViewBag.Employees = JsonConvert.DeserializeObject<EmployeeDepartment>(employee);
            return View(employeeGrade);
        }

        // GET: EmployeeGrade/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new EmployeeGrade");
            var result = await _service.Client.GetAsync("/api/Employees");
            var employees = result.Content.ReadAsStringAsync().Result;
            ViewBag.Employees = JsonConvert.DeserializeObject<List<Employees>>(employees);
            return View();
        }

        // POST: EmployeeGrade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeGrade employeeGrade)
        {
            try
            {
                var postEmployeeGrade = JsonConvert.SerializeObject(employeeGrade);
                var result = await _service.Client.PostAsync("/api/EmployeeGrade", new StringContent(postEmployeeGrade, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeeGrade posted succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeeGrade not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeeGrade");
                return View();
            }
        }

        // GET: EmployeeGrade/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new EmployeeGrade");
            var result = await _service.Client.GetAsync("/api/Employees");
            var employees = result.Content.ReadAsStringAsync().Result;
            ViewBag.Employees = JsonConvert.DeserializeObject<List<Employees>>(employees);
            return View();
        }

        // POST: EmployeeGrade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeGrade employeeGrade)
        {
            try
            {
                var postEmployeeGrade = JsonConvert.SerializeObject(employeeGrade);
                var result = await _service.Client.PutAsync("/api/EmployeeGrade/" + id, new StringContent(postEmployeeGrade, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeeGrade edited succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeeGrade not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeeGrade");
                return View();
            }
        }

        // GET: EmployeeGrade/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about EmployeeGrade with ID:" + id);
            EmployeeGrade employeeGrade = new EmployeeGrade();
            var result = await _service.Client.GetAsync("/api/EmployeeGrade/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about user with ID:" + id);
                var downloadedEmployeeGrade = result.Content.ReadAsStringAsync().Result;
                employeeGrade = JsonConvert.DeserializeObject<EmployeeGrade>(downloadedEmployeeGrade);

            }
            else
            {
                _logger.LogInformation("Deleting information about EmployeeGrade with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(employeeGrade);
        }

        // POST: EmployeeGrade/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EmployeeGrade employee)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/EmployeeGrade/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeeGrade deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeeGrade not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting EmployeeGrade");
                return View();
            }
        }
    }
}