using MediatR;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Entidades;
using NerdStore.Shared.Entidades.DTO;
using NerdStore.Shared.Mediator;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Events;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Application.Commands
{
    public class PedidoCommandHandler :
        IRequestHandler<IniciarPedidoCommand, RespostaPadrao>,
        IRequestHandler<AdicionarItemPedidoCommand, RespostaPadrao>,
        IRequestHandler<AtualizarItemPedidoCommand, RespostaPadrao>,
        IRequestHandler<RemoverItemPedidoCommand, RespostaPadrao>,
        IRequestHandler<AplicarVoucherPedidoCommand, RespostaPadrao>,
        IRequestHandler<FinalizarPedidoCommand, RespostaPadrao>,
        IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, RespostaPadrao>,
        IRequestHandler<CancelarProcessamentoPedidoCommand, RespostaPadrao>
    {
        private readonly IPedidoRepository _pedidoRepository;        

        public PedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;            
        }

        public async Task<RespostaPadrao> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErro(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            var pedidoItem = new PedidoItem(request.ProdutoId, request.NomeProduto, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClienteId);

                pedido.AdicionarItem(pedidoItem);
                if (pedido.EhInvalido)
                    return RequisicaoComErro(pedido.Notifications);

                _pedidoRepository.AdicionarPedido(pedido);
                _pedidoRepository.AdicionarItem(pedidoItem);
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

                _pedidoRepository.AtualizarPedido(pedido);
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, request.ProdutoId,
                                    request.NomeProduto, request.ValorUnitario, request.Quantidade));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        public async Task<RespostaPadrao> Handle(AtualizarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErro(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            if (pedido == null)
                return RequisicaoComErro("pedido não existe");

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, request.ProdutoId);
            if (pedidoItem == null)
                return RequisicaoComErro("Item não existe no pedido");

            pedido.AtualizarUnidades(pedidoItem, request.Quantidade);

            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.AtualizarPedido(pedido);

            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(pedido.ClienteId, pedido.Id, request.ProdutoId, request.Quantidade));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();

        }

        public async Task<RespostaPadrao> Handle(RemoverItemPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErro(request.Notifications);


            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            if (pedido == null)
                return RequisicaoComErro("Pedido não existe");

            var itemPedido = await _pedidoRepository.ObterItemPorPedido(pedido.Id, request.ProdutoId);
            if (itemPedido == null)
                return RequisicaoComErro("Item não existe no pedido");

            pedido.RemoverItem(itemPedido);

            _pedidoRepository.RemoverItem(itemPedido);
            _pedidoRepository.AtualizarPedido(pedido);

            pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(pedido.ClienteId, pedido.Id, request.ProdutoId));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();

        }

        public async Task<RespostaPadrao> Handle(AplicarVoucherPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErro(request.Notifications);

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            if (pedido == null)
                return RequisicaoComErro("Pedido não encontrado");

            var voucher = await _pedidoRepository.ObterVoucherPorCodigo(request.CodigoVoucher);
            if (voucher == null)
                return RequisicaoComErro("Voucher não existe");

            pedido.AplicarVoucher(voucher);
            if (pedido.EhInvalido)
                return RequisicaoComErro(pedido.Notifications);

            _pedidoRepository.AtualizarPedido(pedido);
            pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(pedido.ClienteId, pedido.Id, voucher.Id));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        public async Task<RespostaPadrao> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            request.Validar();
            if (request.EhInvalido)
                return RequisicaoComErro(request.Notifications);

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

        public async Task<RespostaPadrao> Handle(FinalizarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(request.PedidoId);

            if (pedido == null)
                return RequisicaoComErro("Pedido não encontrado");

            pedido.FinalizarPedido();

            pedido.AdicionarEvento(new PedidoFinalizadoEvent(request.PedidoId));

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();

        }

        public async Task<RespostaPadrao> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(request.PedidoId);

            if (pedido == null)
                return RequisicaoComErro("Pedido não encontrado");

            var itens = new List<Item>();
            foreach (var itemPedido in pedido.PedidoItems)
            {
                itens.Add(new Item { ProdutoId = itemPedido.ProdutoId, Quantidade = itemPedido.Quantidade });
            }
            var listaProdutos = new ListaProdutosPedido { Itens = itens, PedidoId = pedido.Id };

            pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.ClienteId, listaProdutos));
            pedido.TornarRascunho();

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        public async Task<RespostaPadrao> Handle(CancelarProcessamentoPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(request.PedidoId);

            if (pedido == null)
                return RequisicaoComErro("Pedido não encontrado");

            pedido.TornarRascunho();

            var commit = await _pedidoRepository.UnitOfWork.Commit();
            if (commit)
                return RequisicaoCompletaComSucesso();

            return RequisicaoComErroCommit();
        }

        private RespostaPadrao RequisicaoCompletaComSucesso()
        {
            return new RespostaPadrao("Requisicão completa com sucesso", true);
        }
        private RespostaPadrao RequisicaoComErro(IReadOnlyCollection<Notification> notifications)
        {
            return new RespostaPadrao("Erro ao processar a requisicao", false, notifications);
        }
        private RespostaPadrao RequisicaoComErro(string mensagem)
        {
            return new RespostaPadrao(mensagem, false);
        }

        private RespostaPadrao RequisicaoComErroCommit()
        {
            return new RespostaPadrao("Erro ao salvar a requisição", false);
        }


    }
}
