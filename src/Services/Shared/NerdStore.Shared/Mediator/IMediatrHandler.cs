
using NerdStore.Shared.Commands;
using NerdStore.Shared.Messaging;
using System.Threading.Tasks;

namespace NerdStore.Shared.Mediator
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;

        Task<RespostaPadrao> EnviarComando<T>(T comando) where T : Command;
    }
}
