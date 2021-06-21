using NerdStore.Shared.Commands;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class FinalizarPedidoCommand : PedidosCommand
    {
        public Guid PedidoId { get; set; }        

        public FinalizarPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            //AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;            
        }
    }
}
