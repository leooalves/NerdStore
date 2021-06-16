using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModel;
using NerdStore.Shared.Commands;
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

            var resposta = await _produtoAppService.AdicionarProduto(produtoViewModel);

            if(resposta.Sucesso)
                return RedirectToAction("Index");


            TempData["Erro"] = ObterMensagensErro(resposta);
            return View(await PopularCategorias(new ProdutoViewModel()));
            
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
            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) 
                return View(await PopularCategorias(produtoViewModel));

            var resposta = await _produtoAppService.AtualizarProduto(produtoViewModel);
            if(resposta.Sucesso)
                return RedirectToAction("Index");

            TempData["Erro"] = ObterMensagensErro(resposta);
            return View(await PopularCategorias(produtoViewModel));
        }


        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.ObterProdutoPorId(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid idProduto, int quantidade)
        {
            var resposta = new RespostaPadrao("", true);
            if(quantidade > 0)
            {
                resposta = await _produtoAppService.ReporEstoqueProduto(idProduto, quantidade);
            }
            else
            {
                resposta = await _produtoAppService.DebitarEstoqueProduto(idProduto, quantidade);
            }

            if(resposta.Sucesso)
                return View("Index", await _produtoAppService.ObterTodosProdutos());

            TempData["Erro"] = ObterMensagensErro(resposta);
            return View("Estoque", await _produtoAppService.ObterProdutoPorId(idProduto));
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = await _produtoAppService.ObterTodasCategorias();
            return produto;
        }

        private string ObterMensagensErro(RespostaPadrao resposta)
        {
            return resposta.Mensagem;
        }
    }
}
