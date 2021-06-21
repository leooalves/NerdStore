using MediatR;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Entidades;
using NerdStore.Shared.Mediator;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class PedidoCommandHandler : 
        IRequestHandler<IniciarPedidoCommand, RespostaPadrao>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
                                    IMediatrHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<RespostaPadrao> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErroValidacao(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            pedido.IniciarPedido();

            _pedidoRepository.AtualizarPedido(pedido);

            var commit = await _pedidoRepository.UnitOfWork.Commit();

            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        private RespostaPadrao RequisicaoCompletaComSucesso()
        {
            return new RespostaPadrao("Requisicão completa com sucesso", true);
        }
        private RespostaPadrao RequisicaoComErroValidacao(IReadOnlyCollection<Notification> notifications)
        {
            return new RespostaPadrao("Erro ao processar a requisicao", false, notifications);
        }
        private RespostaPadrao RequisicaoComErroCommit()
        {
            return new RespostaPadrao("Erro ao salvar a requisição", false);
        }
    }
}
