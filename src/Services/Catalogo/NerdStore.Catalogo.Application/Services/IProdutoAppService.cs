
using NerdStore.Catalogo.Application.ViewModel;
using NerdStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public interface IProdutoAppService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorCategoria(int codigo);
        Task<ProdutoViewModel> ObterProdutoPorId(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutos();
        Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias();

        Task<RespostaPadrao> AdicionarProduto(ProdutoViewModel produtoViewModel);
        Task<RespostaPadrao> AtualizarProduto(ProdutoViewModel produtoViewModel);
        Task<RespostaPadrao> AdicionarCategoria(CategoriaViewModel categoriaViewModel);

        Task<RespostaPadrao> DebitarEstoqueProduto(Guid produtoId, int quantidade);
        Task<RespostaPadrao> ReporEstoqueProduto(Guid produtoId, int quantidade);
    }
}
