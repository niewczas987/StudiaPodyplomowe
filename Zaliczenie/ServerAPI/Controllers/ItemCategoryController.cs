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
    public class ItemCategoryController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public ItemCategoryController(ProductsAndServicesContext context, ILogger<ItemCategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę ItemCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetItemCategory()
        {
            _logger.LogInformation("Get list of ItemCategory");
            return await _context.ItemCategory.ToListAsync();
        }

        /// <summary>
        /// Pobierz ItemCategory o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCategory>> GetItemCategory(long id)
        {
            var itemCategory = await _context.ItemCategory.FindAsync(id);
            _logger.LogInformation("Get ItemCategory with ID:" + id);
            if (itemCategory == null)
            {
                _logger.LogWarning("There's no ItemCategory with ID:" + id);
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ItemCategory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(long id, ItemCategory itemCategory)
        {
            if (id != itemCategory.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");
                return BadRequest();
            }

            _context.Entry(itemCategory).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to ItemCategory with ID:" + id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(id))
                {
                    _logger.LogInformation("There's no ItemCategory with ID:" + id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("There's already ItemCategory with ID:" + id);
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy ItemCategory
        /// </summary>
        /// <param name="itemCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(ItemCategory itemCategory)
        {
            _context.ItemCategory.Add(itemCategory);
            _logger.LogInformation("Adding ItemCategory");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");
            return CreatedAtAction("GetItemCategory", new { id = itemCategory.Id }, itemCategory);
        }

        /// <summary>
        /// Usuń z listy ItemCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemCategory>> DeleteItemCategory(long id)
        {
            var itemCategory = await _context.ItemCategory.FindAsync(id);
            if (itemCategory == null)
            {
                _logger.LogWarning("There's no ItemCategory with ID:" + id);
                return NotFound();
            }

            _context.ItemCategory.Remove(itemCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation("ItemCategory with ID:" + id + " was deleted.");

            return itemCategory;
        }

        private bool ItemCategoryExists(long id)
        {
            return _context.ItemCategory.Any(e => e.Id == id);
        }
    }
}
