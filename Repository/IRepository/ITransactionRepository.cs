using CRUD_Operations.Models;

namespace CRUD_Operations.Repository.IRepository
{
    public interface ITransactionRepository
    {
        // get all students
        Task<IEnumerable<Transaction>> GetAll();
        Task Create(Transaction transaction);
        Task<Transaction>? FindbyId(int? id);

        // Task Delete(Transaction transaction);
        // Task<IEnumerable<Transaction>> Update();
    }
}
