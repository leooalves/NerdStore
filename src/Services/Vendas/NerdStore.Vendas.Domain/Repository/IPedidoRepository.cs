
using NerdStore.Shared.Infra;
using NerdStore.Vendas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPedidoPorId(Guid pedidoId);
        Task<IEnumerable<Pedido>> ObterPedidosPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);

        void AdicionarPedido(Pedido pedido);
        void AtualizarPedido(Pedido pedido);

        Task<PedidoItem> ObterItemPorId(Guid itemPedidoId);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);

        Task<Voucher> ObterVoucherPorCodigo(string codigo);

    }
}
