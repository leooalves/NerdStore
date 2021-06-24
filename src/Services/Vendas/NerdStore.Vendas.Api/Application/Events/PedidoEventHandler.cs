using NerdStore.Shared.Mediator;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Commands;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Application.Events
{
    public class PedidoEventHandler :
        IHandleMessages<PedidoRascunhoIniciadoEvent>,
        IHandleMessages<PedidoItemAdicionadoEvent>,
        IHandleMessages<PedidoEstoqueRejeitadoEvent>,
        IHandleMessages<PedidoPagamentoRealizadoEvent>,
        IHandleMessages<PedidoPagamentoRecusadoEvent>
    {

        private readonly IMediatrHandler _mediatorHandler;
        

        public PedidoEventHandler(IMediatrHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent notification)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent message)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRealizadoEvent message)
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRecusadoEvent message)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
        }
    }
}