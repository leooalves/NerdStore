using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.Catalogo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> TodosProdutos()
        {
            return Ok(await _produtoAppService.ObterTodosProdutos());
        }

        [HttpGet]
        [Route("categoria")]
        public async Task<IActionResult> TodasCategorias()
        {
            return Ok(await _produtoAppService.ObterTodasCategorias());
        }

    }
}
