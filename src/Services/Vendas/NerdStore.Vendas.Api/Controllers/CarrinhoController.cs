using Microsoft.AspNetCore.Mvc;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Mediator;
using NerdStore.Vendas.Api.Application.Commands;
using NerdStore.Vendas.Api.Application.Queries;
using NerdStore.Vendas.Api.Application.Queries.ViewModels;
using System;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarrinhoController : PadraoController
    {
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatrHandler _mediatrHandler;

        public CarrinhoController(IMediatrHandler mediatorHandler, IPedidoQueries pedidoQueries, IMediatrHandler mediatrHandler)
        {
            _pedidoQueries = pedidoQueries;
            _mediatrHandler = mediatrHandler;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<CarrinhoViewModel>> MeuCarrinho()
        {
            return Ok(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<RespostaPadrao>> LimparCarrinho()
        {
            return Ok(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("item")]
        public async Task<ActionResult<RespostaPadrao>> AdicionarItem(AdicionarItemPedidoCommand command)
        {
            command.ClienteId = ClienteId;

            return await _mediatrHandler.EnviarComando(command);
        }

        [HttpDelete]
        [Route("item/{id:guid}")]
        public async Task<ActionResult<RespostaPadrao>> RemoverItem(Guid id)
        {
            var command = new RemoverItemPedidoCommand(ClienteId, id);            

            return await _mediatrHandler.EnviarComando(command);
        }

        [HttpPut]
        [Route("item")]
        public async Task<ActionResult<RespostaPadrao>> Atualizar(AtualizarItemPedidoCommand command)
        {
            command.AtribuiClienteId(ClienteId);

            return await _mediatrHandler.EnviarComando(command);
        }

        [HttpPost]
        [Route("voucher")]
        public async Task<ActionResult<RespostaPadrao>> AplicarVoucher(AplicarVoucherPedidoCommand command)
        {
            command.AtribuiClienteId(ClienteId);

            return await _mediatrHandler.EnviarComando(command);
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<ActionResult<RespostaPadrao>> IniciarPedido(IniciarPedidoCommand command)
        {
            command.AtribuiClienteId(ClienteId);

            return await _mediatrHandler.EnviarComando(command);
        }

    }
}
