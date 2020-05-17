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
    public class ItemController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public ItemController(ProductsAndServicesContext context, ILogger<ItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę Item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            List<Item> items = new List<Item>();
            items = await _context.Item.ToListAsync();
            foreach (Item item in items)
            {
                item.ItemCategoryNavigation = await _context.ItemCategory.FindAsync(item.ItemCategory);
                item.ItemTypeNavigation = await _context.ItemType.FindAsync(item.ItemType);
            }
            _logger.LogInformation("Get list of Items");
            return items;
        }

        /// <summary>
        /// Pobierz Item o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var item = await _context.Item.FindAsync(id);
            _logger.LogInformation("Get Item with ID:" + id);

            if (item == null)
            {
                _logger.LogWarning("There's no Item with ID:" + id);
                return NotFound();
            }
            item.ItemCategoryNavigation = await _context.ItemCategory.FindAsync(item.ItemCategory);
            item.ItemTypeNavigation = await _context.ItemType.FindAsync(item.ItemType);
            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, Item item)
        {
            if (id != item.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to Item with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    _logger.LogInformation("There's no Item with ID:" + id);

                    return NotFound();
                }
                else
                {
                    _logger.LogWarning("There's already Item with ID:" + id);

                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Item.Add(item);
            _logger.LogInformation("Adding Item");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        /// <summary>
        /// Usuń z listy Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(long id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                _logger.LogWarning("There's no Item with ID:" + id);

                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Item with ID:" + id + " was deleted.");

            return item;
        }

        private bool ItemExists(long id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
