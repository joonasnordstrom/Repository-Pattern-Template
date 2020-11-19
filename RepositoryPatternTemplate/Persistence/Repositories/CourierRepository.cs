using Microsoft.EntityFrameworkCore;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Repositories;
using PikiouAPI.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PikiouAPI.Persistence.Repositories
{
    /// <summary>
    /// Implementations for Courier database functionalities
    /// </summary>
    public class CourierRepository : BaseRepository, ICourierRepository
    {
        public CourierRepository(AppDbContext context) : base(context) {}

        /// <summary>
        /// Get all couriers from database
        /// </summary>
        public async Task<IEnumerable<Courier>> ListAsync()
        {
            return await _context.Couriers.ToListAsync();
        }

        /// <summary>
        /// Add courier to database
        /// </summary>
        public async Task AddAsync(Courier courier)
        {
            await _context.Couriers.AddAsync(courier);
        }

        /// <summary>
        /// Get Courier by Id
        /// </summary>
        public async Task<Courier> FindByIdAsync(int id)
        {
            return await _context.Couriers.FindAsync(id);
        }

        /// <summary>
        /// Update existing Courier
        /// </summary>
        public void Update(Courier courier)
        {
            _context.Couriers.Update(courier);
        }

        public void Remove(Courier courier)
        {
            _context.Couriers.Remove(courier);
        }
    }
}
