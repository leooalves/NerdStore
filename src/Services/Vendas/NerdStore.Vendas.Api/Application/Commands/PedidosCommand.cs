using NerdStore.Shared.Commands;
using System;


namespace NerdStore.Vendas.Api.Application.Commands
{
    public abstract class PedidosCommand : Command
    {
        public Guid ClienteId { get; protected set; }

        public void AtribuiClienteId(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
