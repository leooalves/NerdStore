using MediatR;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Entidades;
using NerdStore.Shared.Entidades.DTO;
using NerdStore.Shared.Mediator;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Events;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class PedidoCommandHandler : 
        IRequestHandler<IniciarPedidoCommand, RespostaPadrao>,
        IRequestHandler<AdicionarItemPedidoCommand, RespostaPadrao>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
                                    IMediatrHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<RespostaPadrao> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErroValidacao(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            var pedidoItem = new PedidoItem(request.ProdutoId, request.NomeProduto, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClienteId);

                pedido.AdicionarItem(pedidoItem);
                if (pedido.EhInvalido)
                    return RequisicaoComErroValidacao(pedido.Notifications);

                _pedidoRepository.AdicionarPedido(pedido);
                pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(pedido.ClienteId, pedido.Id));
            }
            else
            {
                var pedidoItemExiste = pedido.PedidoItemExiste(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExiste)
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(item => item.ProdutoId == request.ProdutoId));
                else
                    _pedidoRepository.AdicionarItem(pedidoItem);             
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId,pedido.Id,request.ProdutoId,
                                    request.NomeProduto,request.ValorUnitario,request.Quantidade));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        public async Task<RespostaPadrao> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErroValidacao(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            pedido.IniciarPedido();            

            //setup de objetos para o evento
            var itens = new List<Item>();
            foreach (var pedidoItem in pedido.PedidoItems)
            {
                itens.Add(new Item { ProdutoId = pedidoItem.ProdutoId, Quantidade = pedidoItem.Quantidade });
            }
            var listaProdutosPedido = new ListaProdutosPedido { Itens = itens, PedidoId = pedido.Id };
            pedido.AdicionarEvento(new PedidoIniciadoEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido, pedido.ValorTotal, request.NomeCartao, request.NumeroCartao, request.ExpiracaoCartao, request.CvvCartao));

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
