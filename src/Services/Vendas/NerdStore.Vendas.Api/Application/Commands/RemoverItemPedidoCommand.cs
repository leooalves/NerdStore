using FluentValidation;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class RemoverItemPedidoCommand : PedidosCommand
    {
        public Guid ProdutoId { get; set; }

        public RemoverItemPedidoCommand(Guid clienteId, Guid produtoId)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
        }

        public override void Validar()
        {
            Validar(this, new RemoverItemPedidoValidator());
        }
    }

    public class RemoverItemPedidoValidator : AbstractValidator<RemoverItemPedidoCommand>
    {
        public RemoverItemPedidoValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}
