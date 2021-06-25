using System;

namespace NerdStore.Shared.Messaging.IntegrationEvents
{
    public class ProdutoValorAlteradoEvent : IntegrationEvent
    {
        public ProdutoValorAlteradoEvent(Guid produtoId, decimal novoValor)
        {
            this.ProdutoId = produtoId;
            NovoValor = novoValor;
        }

        public Guid ProdutoId { get; private set; }

        public decimal NovoValor { get; set; }
    }
}