
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Shared.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
