using NerdStore.Shared.Commands;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class CancelarProcessamentoPedidoEstornarEstoqueCommand : PedidosCommand
    {
        public Guid PedidoId { get; set; }        

        public CancelarProcessamentoPedidoEstornarEstoqueCommand(Guid pedidoId, Guid clienteId)
        {
            //AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;            
        }
    }
}
