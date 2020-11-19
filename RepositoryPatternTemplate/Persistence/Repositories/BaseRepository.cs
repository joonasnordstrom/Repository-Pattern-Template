using PikiouAPI.Persistence.Contexts;

namespace PikiouAPI.Persistence.Repositories
{
    /// <summary>
    /// All repositories should be derived from this class. 
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
