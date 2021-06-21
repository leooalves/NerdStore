

using System;
using System.Collections.Generic;

namespace NerdStore.Shared.Entidades.DTO
{
    public class ListaProdutosPedido
    {
        public Guid PedidoId { get; set; }
        public ICollection<Item> Itens { get; set; }
    }
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
