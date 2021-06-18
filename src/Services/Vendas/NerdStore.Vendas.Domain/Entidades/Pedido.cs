using NerdStore.Shared.Entidades;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain.Entidades
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public EPedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        // EF 
        public Voucher Voucher { get; private set; }

        public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
        {
            ClienteId = clienteId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
            _pedidoItems = new List<PedidoItem>();
        }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public bool AplicarVoucher(Voucher voucher)
        {
            voucher.ValidarSeAplicavel();

            if (voucher.EhInvalido)
            {
                this.AddNotifications(voucher.Notifications);
                return false;
            }

            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorPedido();

            return true;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(item => item.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado)
                return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == ETipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public bool PedidoItemExiste(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void AdicionarItem(PedidoItem item)
        {
            if (item.EhInvalido)
            {                
                this.AddNotifications(item.Notifications);
                return;
            }
            
            item.AssociarPedido(Id);

            if (PedidoItemExiste(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            item.CalcularValor();
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            if (item.EhInvalido)
            {
                this.AddNotifications(item.Notifications);
                return;
            }
            
            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            if (itemExistente == null)
            {
                this.AddNotification("Item", "O item não pertence ao pedido");
                return;
            }
                
            _pedidoItems.Remove(itemExistente);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            if (item.EhInvalido)
            {
                AddNotifications(item.Notifications);
                return;
            }
                
            item.AssociarPedido(Id);

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            if (itemExistente == null)
            {
                this.AddNotification("Item", "O item não pertence ao pedido");
                return;
            }

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        public void TornarRascunho()
        {
            PedidoStatus = EPedidoStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            PedidoStatus = EPedidoStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            PedidoStatus = EPedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            PedidoStatus = EPedidoStatus.Cancelado;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }

    }
}
