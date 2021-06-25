using NerdStore.Shared.Mediator;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Commands;
using NerdStore.Vendas.Domain.Repository;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Application.Events
{
    public class PedidoEventHandler :        
        IHandleMessages<PedidoEstoqueRejeitadoEvent>,
        IHandleMessages<PedidoPagamentoRealizadoEvent>,
        IHandleMessages<PedidoPagamentoRecusadoEvent>
    {

        private readonly IMediatrHandler _mediatorHandler;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoEventHandler(IMediatrHandler mediatorHandler, IPedidoRepository pedidoRepository)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoRepository = pedidoRepository;
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