using CRUD_Operations.Models;

namespace CRUD_Operations.Repository.IRepository
{
    public interface ITransactionRepository
    {
        // get all students
        Task<IEnumerable<Transaction>> GetAll();
    }
}
