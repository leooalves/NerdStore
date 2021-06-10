
using NerdStore.Shared.Messaging;
using System.Threading.Tasks;

namespace NerdStore.Shared.Mediator
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
