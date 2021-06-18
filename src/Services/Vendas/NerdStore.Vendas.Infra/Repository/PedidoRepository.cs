

using Microsoft.EntityFrameworkCore;
using NerdStore.Shared.Infra;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Repository;
using NerdStore.Vendas.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Infra.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext _vendasContext;

        public PedidoRepository(VendasContext vendasContext)
        {
            _vendasContext = vendasContext;
        }

        public IUnitOfWork UnitOfWork => _vendasContext;

        public async Task<Pedido> ObterPedidoPorId(Guid pedidoId)
        {
            return await _vendasContext.Pedidos.FindAsync(pedidoId);
        }
        public async Task<IEnumerable<Pedido>> ObterPedidosPorClienteId(Guid clienteId)
        {
            return await _vendasContext.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
        }
        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            return await _vendasContext.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == EPedidoStatus.Rascunho);
        }

        public void AdicionarPedido(Pedido pedido)
        {
            _vendasContext.Pedidos.Add(pedido);
        }
        public void AtualizarPedido(Pedido pedido)
        {
            _vendasContext.Pedidos.Update(pedido);
        }

        public async Task<PedidoItem> ObterItemPorId(Guid itemPedidoId)
        {
            return await _vendasContext.PedidoItems.FindAsync(itemPedidoId);
        }
        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _vendasContext.PedidoItems.FirstOrDefaultAsync(item => item.PedidoId == pedidoId && item.ProdutoId == produtoId);
        }
        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Add(pedidoItem);
        }
        public void AtualizarItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Update(pedidoItem);
        }
        public void RemoverItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Remove(pedidoItem);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _vendasContext.Vouchers.FirstOrDefaultAsync(voucher => voucher.Codigo == codigo);
        }
    }
}
