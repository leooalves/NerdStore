using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Web.MVC.Controllers
{
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("carga-inicial")]
        public async Task<IActionResult> CargaInicial()
        {
            var categoria1 = new CategoriaViewModel
            {
                Codigo = 101,
                Nome = "Camisetas",
                Id = Guid.NewGuid()
            };
            var categoria2 = new CategoriaViewModel
            {
                Codigo = 102,
                Nome = "Canecas",
                Id = Guid.NewGuid()
            };

            await _produtoAppService.AdicionarCategoria(categoria1);
            await _produtoAppService.AdicionarCategoria(categoria2);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {


            return View(await _produtoAppService.ObterTodosProdutos());
        }

        [HttpGet]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoViewModel()));
        }

        [HttpPost]
        [Route("novo-produto")]        
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) 
                return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AdicionarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> EditarProduto(Guid id)
        {
            var produto = await _produtoAppService.ObterProdutoPorId(id);
            return View(await PopularCategorias(produto));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> EditarProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) 
                return View(await PopularCategorias(produtoViewModel));

            var resposta = await _produtoAppService.AtualizarProduto(produtoViewModel);
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.ObterProdutoPorId(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque/")]
        public async Task<IActionResult> AtualizarEstoque(Guid idProduto, int quantidade)
        {
            if(quantidade > 0)
            {
                await _produtoAppService.ReporEstoqueProduto(idProduto, quantidade);
            }
            else
            {
                await _produtoAppService.DebitarEstoqueProduto(idProduto, quantidade);
            }

            return View("Index", await _produtoAppService.ObterTodosProdutos());
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = await _produtoAppService.ObterTodasCategorias();
            return produto;
        }
    }
}
