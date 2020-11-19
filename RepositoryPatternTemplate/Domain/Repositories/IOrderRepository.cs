using PikiouAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> ListAsync();
    }
}
