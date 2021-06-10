using NerdStore.Shared.Entidades;

namespace NerdStore.Shared.Repository
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
