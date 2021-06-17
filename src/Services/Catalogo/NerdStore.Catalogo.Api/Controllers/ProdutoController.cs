using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModel;

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
            var produtos = await _produtoAppService.ObterTodosProdutos();
            return Ok(await PopularCategorias(produtos.ToList()));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProdutoPorId(Guid id)
        {
            var produto = await _produtoAppService.ObterProdutoPorId(id);
            return Ok(await PopularCategorias(produto));
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AtualizaProduto(ProdutoViewModel produto)
        {
            var resposta = await _produtoAppService.AtualizarProduto(produto);
            return Ok(resposta);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CriaProduto(ProdutoViewModel produto)
        {
            var resposta = await _produtoAppService.AdicionarProduto(produto);
            return Ok(resposta);
        }

        [HttpGet]
        [Route("categoria")]
        public async Task<IActionResult> TodasCategorias()
        {
            return Ok(await _produtoAppService.ObterTodasCategorias());
        }        

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            if (produto == null)
                return produto;
            produto.Categorias = await _produtoAppService.ObterTodasCategorias();
            return produto;
        }

        private async Task<List<ProdutoViewModel>> PopularCategorias(List<ProdutoViewModel> produtos)
        {
            if (produtos.Count == 0)
                return produtos;
            var categorias = await _produtoAppService.ObterTodasCategorias();
            foreach (var produto in produtos)
            {
                produto.Categorias = categorias;
            }
            
            return produtos;
        }

    }
}
