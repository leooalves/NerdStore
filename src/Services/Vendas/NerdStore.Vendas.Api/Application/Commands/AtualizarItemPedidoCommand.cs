using FluentValidation;
using System;


namespace NerdStore.Vendas.Api.Application.Commands
{
    public class AtualizarItemPedidoCommand : PedidosCommand
    {        
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public AtualizarItemPedidoCommand( Guid produtoId, int quantidade, Guid clienteId)
        {            
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ClienteId = clienteId;
        }

        public override void Validar()
        {
            Validar(this, new AtualizarItemPedidoValidator());
        }
    }
    public class AtualizarItemPedidoValidator : AbstractValidator<AtualizarItemPedidoCommand>
    {
        public AtualizarItemPedidoValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");
        }
    }
}
