using FluentValidation;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class AplicarVoucherPedidoCommand : PedidosCommand
    {
        public string CodigoVoucher { get; set; }

        public AplicarVoucherPedidoCommand(string codigoVoucher, Guid clienteId)
        {
            CodigoVoucher = codigoVoucher;
            ClienteId = clienteId;
        }

        public override void Validar()
        {
            Validar(this, new AplicarVoucherPedidoValidator());
        }
    }

    public class AplicarVoucherPedidoValidator : AbstractValidator<AplicarVoucherPedidoCommand>
    {
        public AplicarVoucherPedidoValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.CodigoVoucher)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}
