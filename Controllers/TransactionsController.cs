using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Operations.Data;
using CRUD_Operations.Models;
using CRUD_Operations.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace CRUD_Operations.Controllers
{
    // [Authorize] has been added, so that user with accounts can access it.
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ApplicationDbContext context, ITransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            // This methid gets all stored records in sql server
              return _context.Transactions != null ? 
                          View(await _transactionRepository.GetAll()) :
                          Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(transaction);
                //await _context.SaveChangesAsync();
                await _transactionRepository.Create(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Transactions == null)
            //{
            //    return NotFound();
            //}

            var transaction = await _transactionRepository.FindbyId(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            // TODO: This needs to be done
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Transaction obj = await _transactionRepository.Edit(id, transaction);
                if (obj == null)
                {
                    return NotFound();
                }

                //try
                //{
                //    _context.Update(transaction);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!TransactionExists(transaction.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null || _context.Transactions == null)
            //{
            //    return NotFound();
            //}

            //var transaction = await _context.Transactions
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (transaction == null)
            //{
            //    return NotFound();
            //}
            Transaction transaction = await _transactionRepository.Delete(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Transactions == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            //}

            bool transactionStatus = _transactionRepository.CheckTransactionExist();
            if (!transactionStatus)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }

            //var transaction = await _context.Transactions.FindAsync(id);
            //if (transaction != null)
            //{
            //    _context.Transactions.Remove(transaction);
            //}
            
            //await _context.SaveChangesAsync();

            await _transactionRepository.ConformDelete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
