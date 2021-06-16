

using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Shared.Mediator;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Service
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatrHandler _mediatr;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatrHandler bus)
        {
            _produtoRepository = produtoRepository;
            _mediatr = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            if (produto == null) return false;            

            produto.DebitarEstoque(quantidade);

            if (produto.EhInvalido)
                return false;
            
            if (produto.QuantidadeEstoque <= produto.QuantidadeMinimaReporEstoque)
            {
                await _mediatr.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque, produto.QuantidadeMinimaReporEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }      
    }
}
