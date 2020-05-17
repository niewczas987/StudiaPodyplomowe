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
    public class TransactionController : ControllerBase
    {
        private readonly ProductsAndServicesContext _context;
        private readonly ILogger _logger;

        public TransactionController(ProductsAndServicesContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobierz listę Transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions()
        {
            List<Transactions> transactions = new List<Transactions>();
            transactions = await _context.Transactions.ToListAsync();
            foreach (Transactions transaction in transactions)
            {
                transaction.IdItemNavigation = await _context.Item.FindAsync(transaction.IdItem);
            }
            _logger.LogInformation("Get list of Transactions");

            return transactions;
        }

        /// <summary>
        /// Pobierz Transaction o wybranym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> GetTransactions(long id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            _logger.LogInformation("Get Transactions with ID:" + id);

            if (transactions == null)
            {
                _logger.LogWarning("There's no Transactions with ID:" + id);

                return NotFound();
            }
            transactions.IdItemNavigation = await _context.Item.FindAsync(transactions.IdItem);
            return transactions;
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions(long id, Transactions transactions)
        {
            if (id != transactions.Id)
            {
                _logger.LogWarning("Incorrect id on put request.");

                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Saving data to Transactions with ID:" + id);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
                {
                    _logger.LogInformation("There's no Transactions with ID:" + id);

                    return NotFound();
                }
                else
                {
                    _logger.LogWarning("There's already Transactions with ID:" + id);

                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodaj do listy Transactions
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Transactions>> PostTransactions(Transactions transactions)
        {
            _logger.LogInformation("Adding Transaction");

            _context.Transactions.Add(transactions);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saving changes");

            return CreatedAtAction("GetTransactions", new { id = transactions.Id }, transactions);
        }

        /// <summary>
        /// Usuń z listy Transactions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transactions>> DeleteTransactions(long id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                _logger.LogWarning("There's no Transactions with ID:" + id);

                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Transaction with ID:" + id + " was deleted.");

            return transactions;
        }

        private bool TransactionsExists(long id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
