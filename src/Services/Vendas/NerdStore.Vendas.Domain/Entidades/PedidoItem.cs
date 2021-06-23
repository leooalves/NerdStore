

using FluentValidation;
using NerdStore.Shared.Entidades;
using System;

namespace NerdStore.Vendas.Domain.Entidades
{
    public class PedidoItem : Entity
    {
       
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Guid PedidoId { get; private set; }
        //para o EF
        public Pedido Pedido { get; set; }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;

            Validar(this, new PedidoItemValidator());
        }

        protected PedidoItem() { }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }
    }

    public class PedidoItemValidator : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidator()
        {
            RuleFor(item => item.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade de produtos deve ser maior do que zero");
            RuleFor(item => item.ValorUnitario)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor unitario do item deve ser maior ou igual a zero");

        }
    }
}
