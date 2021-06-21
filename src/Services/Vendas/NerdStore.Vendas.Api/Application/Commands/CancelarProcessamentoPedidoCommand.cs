using NerdStore.Shared.Commands;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class CancelarProcessamentoPedidoCommand : PedidosCommand
    {
        public Guid PedidoId { get; set; }        

        public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            //AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;            
        }
    }
}
