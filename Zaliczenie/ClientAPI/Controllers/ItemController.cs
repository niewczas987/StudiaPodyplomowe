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
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public ItemController(IAPIService service, ILogger<ItemController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: Item
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of Items");
            List<Item> item = new List<Item>();
            var result = await _service.Client.GetAsync("/api/Item");
            if (result.IsSuccessStatusCode)
            {
                var downloadedItem = result.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<List<Item>>(downloadedItem);
                _logger.LogInformation("Item downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading Item. ErrorStatusCode:" + result.StatusCode);
            }
            return View(item);

        }

        // GET: Item/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of Item with ID:" + id);
            Item item = new Item();
            var result = await _service.Client.GetAsync("/api/Item/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of Item with ID:" + id);
                var downloadedItem = result.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Item>(downloadedItem);

            }
            else
            {
                _logger.LogInformation("Downloading details of Item with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(item);
        }

        // GET: Item/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new Item");
            var result = await _service.Client.GetAsync("/api/ItemType");
            var itemType = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfTypes = JsonConvert.DeserializeObject<List<ItemType>>(itemType);
            result = await _service.Client.GetAsync("/api/ItemCategory");
            var itemCategory = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategory);

            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {
            try
            {
                var postItem = JsonConvert.SerializeObject(item);
                var result = await _service.Client.PostAsync("/api/Item", new StringContent(postItem, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Item posted succesfully");
                }
                else
                {
                    _logger.LogInformation("Item not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Item");
                return View();
            }
        }

        // GET: Item/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to edit Item");
            var result = await _service.Client.GetAsync("/api/ItemType");
            var itemType = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfTypes = JsonConvert.DeserializeObject<List<ItemType>>(itemType);
            result = await _service.Client.GetAsync("/api/ItemCategory");
            var itemCategory = result.Content.ReadAsStringAsync().Result;
            ViewBag.listOfCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategory);
            result = await _service.Client.GetAsync("/api/Item/" + id);
            var item = result.Content.ReadAsStringAsync().Result;
            ViewBag.Item = JsonConvert.DeserializeObject<Item>(item);
            return View();
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Item Item)
        {
            try
            {
                var postItem = JsonConvert.SerializeObject(Item);
                var result = await _service.Client.PutAsync("/api/Item/" + id, new StringContent(postItem, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Item edited succesfully");
                }
                else
                {
                    _logger.LogInformation("Item not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting Item");
                return View();
            }
        }

        // GET: Item/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about Item with ID:" + id);
            Item item = new Item();
            var result = await _service.Client.GetAsync("/api/Item/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about Item with ID:" + id);
                var downloadedItem = result.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Item>(downloadedItem);

            }
            else
            {
                _logger.LogInformation("Deleting information about Item with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Item item)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/Item/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Item deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("Item not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting Item");
                return View();
            }
        }
    }
}