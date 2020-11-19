using PikiouAPI.Domain.Repositories;
using PikiouAPI.Persistence.Contexts;
using System.Threading.Tasks;

namespace PikiouAPI.Persistence.Repositories
{
    /// <summary>
    /// This class is used to handle all changes to database at once. Check:
    /// https://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
