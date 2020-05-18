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
    public class EmployeePositionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public EmployeePositionController(IAPIService service, ILogger<EmployeePositionController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: EmployeePosition
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of EmployeePosition");
            List<EmployeePosition> employeePosition = new List<EmployeePosition>();
            var result = await _service.Client.GetAsync("/api/EmployeePosition");
            if (result.IsSuccessStatusCode)
            {
                var downloadedEmployeePosition = result.Content.ReadAsStringAsync().Result;
                employeePosition = JsonConvert.DeserializeObject<List<EmployeePosition>>(downloadedEmployeePosition);
                _logger.LogInformation("EmployeePosition downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading EmployeePosition. ErrorStatusCode:" + result.StatusCode);
            }
            return View(employeePosition);

        }

        // GET: EmployeePosition/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of EmployeePosition with ID:" + id);
            EmployeePosition employeePosition = new EmployeePosition();
            var result = await _service.Client.GetAsync("/api/EmployeePosition/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of EmployeePosition with ID:" + id);
                var downloadedEmployeePosition = result.Content.ReadAsStringAsync().Result;
                employeePosition = JsonConvert.DeserializeObject<EmployeePosition>(downloadedEmployeePosition);

            }
            else
            {
                _logger.LogInformation("Downloading details of EmployeePosition with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(employeePosition);
        }

        // GET: EmployeePosition/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new EmployeePosition");
            return View();
        }

        // POST: EmployeePosition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeePosition employeePosition)
        {
            try
            {
                var postEmployeePosition = JsonConvert.SerializeObject(employeePosition);
                var result = await _service.Client.PostAsync("/api/EmployeePosition", new StringContent(postEmployeePosition, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeePosition posted succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeePosition not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeePosition");
                return View();
            }
        }

        // GET: EmployeePosition/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new EmployeePosition");
            var result = await _service.Client.GetAsync("/api/EmployeePosition/" + id);
            var employeePosition = result.Content.ReadAsStringAsync().Result;
            ViewBag.EmployeePosition = JsonConvert.DeserializeObject<EmployeePosition>(employeePosition);
            return View();
        }

        // POST: EmployeePosition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeePosition employeePosition)
        {
            try
            {
                var postEmployeePosition = JsonConvert.SerializeObject(employeePosition);
                var result = await _service.Client.PutAsync("/api/EmployeePosition/" + id, new StringContent(postEmployeePosition, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeePosition edited succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeePosition not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting EmployeePosition");
                return View();
            }
        }

        // GET: EmployeePosition/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about EmployeePosition with ID:" + id);
            EmployeePosition employeePosition = new EmployeePosition();
            var result = await _service.Client.GetAsync("/api/EmployeePosition/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about EmployeePosition with ID:" + id);
                var downloadedEmployeePosition = result.Content.ReadAsStringAsync().Result;
                employeePosition = JsonConvert.DeserializeObject<EmployeePosition>(downloadedEmployeePosition);

            }
            else
            {
                _logger.LogInformation("Deleting information about EmployeePosition with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(employeePosition);
        }

        // POST: EmployeePosition/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EmployeePosition employeePosition)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/EmployeePosition/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("EmployeePosition deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("EmployeePosition not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting EmployeePosition");
                return View();
            }
        }
    }
}