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
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public TransactionController(IAPIService service, ILogger<TransactionController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: Transaction
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of Transaction");
            List<Transaction> transaction = new List<Transaction>();
            var result = await _service.Client.GetAsync("/api/Transaction");
            if (result.IsSuccessStatusCode)
            {
                var downloadedTransaction = result.Content.ReadAsStringAsync().Result;
                transaction = JsonConvert.DeserializeObject<List<Transaction>>(downloadedTransaction);
                _logger.LogInformation("Transaction downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading Transaction. ErrorStatusCode:" + result.StatusCode);
            }
            return View(transaction);

        }

        // GET: Transaction/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of Transaction with ID:" + id);
            Transaction transaction = new Transaction();
            var result = await _service.Client.GetAsync("/api/Transaction/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of Transaction with ID:" + id);
                var downloadedTransaction = result.Content.ReadAsStringAsync().Result;
                transaction = JsonConvert.DeserializeObject<Transaction>(downloadedTransaction);

            }
            else
            {
                _logger.LogInformation("Downloading details of Transaction with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new Transaction");
            var result = await _service.Client.GetAsync("/api/Item");
            var item = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfItems = JsonConvert.DeserializeObject<List<Item>>(item);
            result = await _service.Client.GetAsync("/api/Transaction");
            var transaction = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfTransaction = JsonConvert.DeserializeObject<List<Transaction>>(transaction);
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Transaction transaction)
        {
            try
            {
                var postTransaction = JsonConvert.SerializeObject(transaction);
                var result = await _service.Client.PostAsync("/api/Transaction", new StringContent(postTransaction, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Transaction posted succesfully");
                }
                else
                {
                    _logger.LogInformation("Transaction not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Transaction");
                return View();
            }
        }

        // GET: Transaction/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new Transaction");
            var result = await _service.Client.GetAsync("/api/Item");
            var item = result.Content.ReadAsStringAsync().Result;
            ViewBag.Item = JsonConvert.DeserializeObject<List<Item>>(item);
            result = await _service.Client.GetAsync("/api/Transaction/"+id);
            var transaction = result.Content.ReadAsStringAsync().Result;
            ViewBag.Transaction = JsonConvert.DeserializeObject<Transaction>(transaction);
            return View();
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Transaction transaction)
        {
            try
            {
                var postTransaction = JsonConvert.SerializeObject(transaction);
                var result = await _service.Client.PutAsync("/api/Transaction/" + id, new StringContent(postTransaction, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Transaction edited succesfully");
                }
                else
                {
                    _logger.LogInformation("Transaction not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Transaction");
                return View();
            }
        }

        // GET: Transaction/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about Transaction with ID:" + id);
            Transaction transaction = new Transaction();
            var result = await _service.Client.GetAsync("/api/Transaction/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about Transaction with ID:" + id);
                var downloadedTransaction = result.Content.ReadAsStringAsync().Result;
                transaction = JsonConvert.DeserializeObject<Transaction>(downloadedTransaction);

            }
            else
            {
                _logger.LogInformation("Deleting information about Transaction with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Transaction Transaction)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/Transaction/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Transaction deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("Transaction not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting Transaction");
                return View();
            }
        }
    }
}