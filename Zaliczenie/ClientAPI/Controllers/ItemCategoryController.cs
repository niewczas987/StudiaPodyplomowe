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
    public class ItemCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public ItemCategoryController(IAPIService service, ILogger<ItemCategoryController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: ItemCategory
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of ItemCategory");
            List<ItemCategory> itemCategory = new List<ItemCategory>();
            var result = await _service.Client.GetAsync("/api/ItemCategory");
            if (result.IsSuccessStatusCode)
            {
                var downloadedItemCategory = result.Content.ReadAsStringAsync().Result;
                itemCategory = JsonConvert.DeserializeObject<List<ItemCategory>>(downloadedItemCategory);
                _logger.LogInformation("ItemCategory downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading ItemCategory. ErrorStatusCode:" + result.StatusCode);
            }
            return View(itemCategory);

        }

        // GET: ItemCategory/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of ItemCategory with ID:" + id);
            ItemCategory itemCategory = new ItemCategory();
            var result = await _service.Client.GetAsync("/api/ItemCategory/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of ItemCategory with ID:" + id);
                var downloadedItemCategory = result.Content.ReadAsStringAsync().Result;
                itemCategory = JsonConvert.DeserializeObject<ItemCategory>(downloadedItemCategory);

            }
            else
            {
                _logger.LogInformation("Downloading details of ItemCategory with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(itemCategory);
        }

        // GET: ItemCategory/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new ItemCategory");
            return View();
        }

        // POST: ItemCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemCategory itemCategory)
        {
            try
            {
                var postItemCategory = JsonConvert.SerializeObject(itemCategory);
                var result = await _service.Client.PostAsync("/api/ItemCategory", new StringContent(postItemCategory, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemCategory posted succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemCategory not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting ItemCategory");
                return View();
            }
        }

        // GET: ItemCategory/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to Create new ItemCategory");
            var result = await _service.Client.GetAsync("/api/ItemCategory/"+id);
            var itemCategory = result.Content.ReadAsStringAsync().Result;
            ViewBag.ItemCategory = JsonConvert.DeserializeObject<ItemCategory>(itemCategory);
            return View();
        }

        // POST: ItemCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ItemCategory itemCategory)
        {
            try
            {
                var postItemCategory = JsonConvert.SerializeObject(itemCategory);
                var result = await _service.Client.PutAsync("/api/ItemCategory/" + id, new StringContent(postItemCategory, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemCategory edited succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemCategory not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting ItemCategory");
                return View();
            }
        }

        // GET: ItemCategory/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about ItemCategory with ID:" + id);
            ItemCategory itemCategory = new ItemCategory();
            var result = await _service.Client.GetAsync("/api/ItemCategory/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about ItemCategory with ID:" + id);
                var downloadedItemCategory = result.Content.ReadAsStringAsync().Result;
                itemCategory = JsonConvert.DeserializeObject<ItemCategory>(downloadedItemCategory);

            }
            else
            {
                _logger.LogInformation("Deleting information about ItemCategory with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(itemCategory);
        }

        // POST: ItemCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ItemCategory itemCategory)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/ItemCategory/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemCategory deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemCategory not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting ItemCategory");
                return View();
            }
        }
    }
}