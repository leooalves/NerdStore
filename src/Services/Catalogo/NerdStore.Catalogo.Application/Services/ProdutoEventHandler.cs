

using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Shared.Messaging.IntegrationEvents;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoEventHandler :
        IHandleMessages<ProdutoValorAlteradoEvent>,
        IHandleMessages<PedidoIniciadoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IBus _bus;

        public ProdutoEventHandler(IProdutoRepository produtoRepository, IBus bus, IProdutoAppService produtoAppService)
        {
            _produtoRepository = produtoRepository;
            _bus = bus;
            _produtoAppService = produtoAppService;
        }


        public async Task Handle(ProdutoValorAlteradoEvent message)
        {
            //para test do rebus
            var produto = await _produtoRepository.ObterProdutoPorId(message.ProdutoId);

            produto.AlterarDescricao(produto.Descricao + " valor alterado");

            _produtoRepository.Atualizar(produto);


            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task Handle(PedidoIniciadoEvent message)
        {
            var resposta = await _produtoAppService.DebitarEstoqueListaProdutos(message.ProdutosPedido);

            if (resposta.Sucesso)
                await _bus.Publish(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total, message.ProdutosPedido, message.NomeCartao,
                    message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));
            else
                await _bus.Publish(new PedidoEstoqueRejeitadoEvent(message.PedidoId,message.ClienteId));

        }
    }
}
