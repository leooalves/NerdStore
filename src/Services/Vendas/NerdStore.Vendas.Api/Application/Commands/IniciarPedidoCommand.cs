using FluentValidation;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class IniciarPedidoCommand : PedidosCommand
    {
        public Guid PedidoId { get; private set; }        
        public decimal Total { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public IniciarPedidoCommand(Guid pedidoId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public override void Validar()
        {
            Validar(this, new IniciarPedidoValidator());
        }
    }

    public class IniciarPedidoValidator : AbstractValidator<IniciarPedidoCommand>
    {
        public IniciarPedidoValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");

            RuleFor(c => c.NomeCartao)
                .NotEmpty()
                .WithMessage("O nome no cartão não foi informado");

            RuleFor(c => c.NumeroCartao)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            RuleFor(c => c.ExpiracaoCartao)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            RuleFor(c => c.CvvCartao)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}
