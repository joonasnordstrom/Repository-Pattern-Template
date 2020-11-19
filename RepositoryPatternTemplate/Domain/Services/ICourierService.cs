using System.Collections.Generic;
using System.Threading.Tasks;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Services.Communication;

namespace PikiouAPI.Domain.Services
{
    /// <summary>
    /// Declarations for handling database functions for Courier
    /// </summary>
    public interface ICourierService
    {
        Task<IEnumerable<Courier>> ListAsync();
        Task<CourierResponse> SaveAsync(Courier courier);
        Task<CourierResponse> UpdateAsync(int id, Courier courier);
        Task<CourierResponse> DeleteAsync(int id);
    }
}
