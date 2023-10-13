using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        // This is where we will be addig the models
    }
}
