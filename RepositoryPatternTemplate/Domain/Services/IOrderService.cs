using PikiouAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> ListAsync();
    }
}
