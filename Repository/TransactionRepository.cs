using CRUD_Operations.Data;
using CRUD_Operations.Models;
using CRUD_Operations.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUD_Operations.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        // let's first get the _logger and dbContext
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Transaction transaction)
        {
            _dbContext.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Transaction>? Delete(int? id)
        {
            // TODO: we are implementing this one
            if (id == null || _dbContext.Transactions == null)
            {
                return null;
            }

            var transaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return null;
            }

            return transaction;
        }

        public async Task<Transaction>? Edit(int id, Transaction transaction)
        {
            try
            {
                _dbContext.Update(transaction);
                await _dbContext.SaveChangesAsync();
                return transaction;
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Transaction>? FindbyId(int? id)
        {
            // check if the id exists
            if (id == null || _dbContext.Transactions == null)
            {
                return null;
            }

            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                return transaction;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            IEnumerable<Transaction> obj = (IEnumerable<Transaction>)await _dbContext.Transactions.ToListAsync();
            // throw new NotImplementedException();
            return obj;
        }

        private bool TransactionExists(int id)
        {
            return (_dbContext.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}