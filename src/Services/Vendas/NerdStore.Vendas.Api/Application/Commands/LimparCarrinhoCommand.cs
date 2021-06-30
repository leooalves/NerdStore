using FluentValidation;
using System;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class LimparCarrinhoCommand : PedidosCommand {
        public LimparCarrinhoCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override void Validar()
        {
            Validar(this, new LimparCarrinhoCommandValidator());
        }
    }

    public class LimparCarrinhoCommandValidator : AbstractValidator<LimparCarrinhoCommand>
    {
        public LimparCarrinhoCommandValidator()
        {
            RuleFor(c => c.ClienteId)
             .NotEqual(Guid.Empty)
             .WithMessage("Id do cliente inválido");
        }
    }
}
