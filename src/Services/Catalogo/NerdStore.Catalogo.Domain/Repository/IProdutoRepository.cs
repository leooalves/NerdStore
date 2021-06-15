
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Shared.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodosProdutos();
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
