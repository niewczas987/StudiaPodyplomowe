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
    public class ItemTypeController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;


        public ItemTypeController(ProductsAndServicesContext context, ILogger<ItemTypeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę ItemType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemType>>> GetItemType()
        {
            _logger.LogInformation("Get list of ItemType");
            return await _context.ItemType.ToListAsync();
        }

        /// <summary>
        /// Pobierz ItemType o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemType>> GetItemType(long id)
        {
            var itemType = await _context.ItemType.FindAsync(id);
            _logger.LogInformation("Get ItemType with ID:" + id);

            if (itemType == null)
            {
                _logger.LogWarning("There's no ItemType with ID:" + id);

                return NotFound();
            }

            return itemType;
        }

        // PUT: api/ItemType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType(long id, ItemType itemType)
        {
            if (id != itemType.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");

                return BadRequest();
            }

            _context.Entry(itemType).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to ItemType with ID:" + id);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemTypeExists(id))
                {
                    _logger.LogInformation("There's no ItemType with ID:" + id);

                    return NotFound();
                }
                else
                {
                    _logger.LogWarning("There's already ItemType with ID:" + id);

                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy ItemType
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ItemType>> PostItemType(ItemType itemType)
        {
            _context.ItemType.Add(itemType);
            _logger.LogInformation("Adding ItemType");

            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");

            return CreatedAtAction("GetItemType", new { id = itemType.Id }, itemType);
        }

        /// <summary>
        /// Usuń z listy ItemType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemType>> DeleteItemType(long id)
        {
            var itemType = await _context.ItemType.FindAsync(id);
            if (itemType == null)
            {
                _logger.LogWarning("There's no ItemType with ID:" + id);

                return NotFound();
            }

            _context.ItemType.Remove(itemType);
            await _context.SaveChangesAsync();
            _logger.LogInformation("ItemType with ID:" + id + " was deleted.");

            return itemType;
        }

        private bool ItemTypeExists(long id)
        {
            return _context.ItemType.Any(e => e.Id == id);
        }
    }
}
