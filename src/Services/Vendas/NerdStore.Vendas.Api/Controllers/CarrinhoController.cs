using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Mediator;
using NerdStore.Vendas.Api.Application.Commands;
using NerdStore.Vendas.Api.Application.Queries;
using NerdStore.Vendas.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<RespostaPadrao>> AdicionarItem(AdicionarItemPedidoCommand command)
        {
            //command.

            return await _mediatrHandler.EnviarComando(command);
        }


    }
}
