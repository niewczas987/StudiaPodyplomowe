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
    public class ItemTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IAPIService _service;
        private ILogger _logger;

        public ItemTypeController(IAPIService service, ILogger<ItemTypeController> logger, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        // GET: ItemType
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Downloading list of ItemType");
            List<ItemType> itemType = new List<ItemType>();
            var result = await _service.Client.GetAsync("/api/ItemType");
            if (result.IsSuccessStatusCode)
            {
                var downloadedItemType = result.Content.ReadAsStringAsync().Result;
                itemType = JsonConvert.DeserializeObject<List<ItemType>>(downloadedItemType);
                _logger.LogInformation("ItemType downloaded succesfully");
            }
            else
            {
                _logger.LogInformation("Error downloading ItemType. ErrorStatusCode:" + result.StatusCode);
            }
            return View(itemType);

        }

        // GET: ItemType/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Downloading details of ItemType with ID:" + id);
            ItemType itemType = new ItemType();
            var result = await _service.Client.GetAsync("/api/ItemType/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Downloaded details of ItemType with ID:" + id);
                var downloadedItemType = result.Content.ReadAsStringAsync().Result;
                itemType = JsonConvert.DeserializeObject<ItemType>(downloadedItemType);

            }
            else
            {
                _logger.LogInformation("Downloading details of ItemType with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(itemType);
        }

        // GET: ItemType/Create
        public async Task<ActionResult> Create()
        {
            _logger.LogInformation("Entrying data to Create new ItemType");
            return View();
        }

        // POST: ItemType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemType itemType)
        {
            try
            {
                var postItemType = JsonConvert.SerializeObject(itemType);
                var result = await _service.Client.PostAsync("/api/ItemType", new StringContent(postItemType, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemType posted succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemType not posted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting ItemType");
                return View();
            }
        }

        // GET: ItemType/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Entrying data to edit ItemType");
            var result = await _service.Client.GetAsync("/api/ItemType/" + id);
            var ItemType = result.Content.ReadAsStringAsync().Result;
            ViewBag.ItemType = JsonConvert.DeserializeObject<ItemType>(ItemType);
            return View();
        }

        // POST: ItemType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ItemType ItemType)
        {
            try
            {
                var postItemType = JsonConvert.SerializeObject(ItemType);
                var result = await _service.Client.PutAsync("/api/ItemType/" + id, new StringContent(postItemType, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemType edited succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemType not edited. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during posting ItemType");
                return View();
            }
        }

        // GET: ItemType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting information about ItemType with ID:" + id);
            ItemType itemType = new ItemType();
            var result = await _service.Client.GetAsync("/api/ItemType/" + id);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Information about ItemType with ID:" + id);
                var downloadedItemType = result.Content.ReadAsStringAsync().Result;
                itemType = JsonConvert.DeserializeObject<ItemType>(downloadedItemType);

            }
            else
            {
                _logger.LogInformation("Deleting information about ItemType with ID:" + id + " ended with error. ErrorCode:" + result.StatusCode);
            }
            return View(itemType);
        }

        // POST: ItemType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ItemType itemType)
        {
            try
            {
                var result = await _service.Client.DeleteAsync("/api/ItemType/" + id);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ItemType deleted succesfully");
                }
                else
                {
                    _logger.LogInformation("ItemType not deleted. ErrorCode:" + result.StatusCode);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogWarning("Error during deleting ItemType");
                return View();
            }
        }
    }
}