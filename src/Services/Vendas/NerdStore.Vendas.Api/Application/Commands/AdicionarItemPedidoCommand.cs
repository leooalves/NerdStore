using FluentValidation;
using NerdStore.Shared.Commands;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public Guid ClienteId { get; set; }
        public AdicionarItemPedidoCommand(Guid produtoId, Guid clienteId, string nome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            NomeProduto = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ClienteId = clienteId;
        }

        public override void Validar()
        {
            Validar(this, new AdicionarItemPedidoValidator());
        }

    }

    public class AdicionarItemPedidoValidator : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public AdicionarItemPedidoValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.NomeProduto)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
