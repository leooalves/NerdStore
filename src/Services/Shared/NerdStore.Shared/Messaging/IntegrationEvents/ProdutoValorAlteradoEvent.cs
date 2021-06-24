using System;

namespace NerdStore.Shared.Messaging.IntegrationEvents
{
    public class ProdutoValorAlteradoEvent : IntegrationEvent
    {
        public ProdutoValorAlteradoEvent(Guid produtoId)
        {
            this.ProdutoId = produtoId;
        }

        public Guid ProdutoId { get; private set; }
    }
}