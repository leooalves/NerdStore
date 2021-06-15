using NerdStore.Shared.Entidades;

namespace NerdStore.Shared.Infra
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
