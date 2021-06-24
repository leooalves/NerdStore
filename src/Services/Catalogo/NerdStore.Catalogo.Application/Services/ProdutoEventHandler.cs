

using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Shared.Messaging.IntegrationEvents;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoEventHandler : IHandleMessages<ProdutoValorAlteradoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoValorAlteradoEvent message)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(message.ProdutoId);

            produto.AlterarDescricao(produto.Descricao + "valor alterado");

            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }
    }
}
