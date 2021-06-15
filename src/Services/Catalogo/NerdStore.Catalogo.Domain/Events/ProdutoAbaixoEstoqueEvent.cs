

using NerdStore.Shared.Messaging;
using System;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }
        public int QuantidadeMinimaNotificacao { get; private set; }

        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante, int quantidadeMinima) : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
            QuantidadeMinimaNotificacao = quantidadeMinima;
        }
    }
}
