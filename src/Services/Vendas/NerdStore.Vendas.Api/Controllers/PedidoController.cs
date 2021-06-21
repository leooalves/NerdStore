using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Shared.Mediator;
using NerdStore.Vendas.Api.Application.Queries;
using NerdStore.Vendas.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PedidoController : PadraoController
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<PedidoViewModel>> TodosPedidosPorCliente()
        {
            return await _pedidoQueries.ObterPedidosCliente(ClienteId);
        }
    }
}
