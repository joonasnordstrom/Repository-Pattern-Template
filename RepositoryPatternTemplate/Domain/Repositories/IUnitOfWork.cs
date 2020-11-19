using System.Threading.Tasks;

namespace PikiouAPI.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
