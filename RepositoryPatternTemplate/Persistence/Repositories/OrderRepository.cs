using Microsoft.EntityFrameworkCore;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Repositories;
using PikiouAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _context.Orders
                .ToListAsync();
        }

    }
}
