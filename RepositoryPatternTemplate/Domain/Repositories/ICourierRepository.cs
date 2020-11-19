using PikiouAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Repositories
{
    public interface ICourierRepository
    {
        Task<IEnumerable<Courier>> ListAsync();
        Task AddAsync(Courier courier);
        Task<Courier> FindByIdAsync(int id);

        void Update(Courier courier);
        void Remove(Courier courier);
    }
}
