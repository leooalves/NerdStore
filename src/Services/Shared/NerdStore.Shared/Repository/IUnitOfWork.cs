
using System.Threading.Tasks;

namespace NerdStore.Shared.Repository
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
