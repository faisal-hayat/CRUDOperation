using CRUD_Operations.Models;

namespace CRUD_Operations.Repository.IRepository
{
    public interface ITransactionRepository
    {
        // get all students
        Task<IEnumerable<Transaction>> GetAll();
        Task Create(Transaction transaction);
        Task<Transaction>? FindbyId(int? id);
        Task<Transaction>? Edit(int id, Transaction transaction);

        Task<Transaction>? Delete(int? id);
        Task ConformDelete(int id);
        bool CheckTransactionExist();
        // Task<IEnumerable<Transaction>> Update();
    }
}
