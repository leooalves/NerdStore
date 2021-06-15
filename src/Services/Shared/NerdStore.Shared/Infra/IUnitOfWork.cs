
using System.Threading.Tasks;

namespace NerdStore.Shared.Infra
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
