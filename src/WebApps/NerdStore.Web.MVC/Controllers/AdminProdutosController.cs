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
        #region CargaInicial

        [HttpGet]
        [Route("carga-inicial")]
        public async Task<IActionResult> CargaInicial()
        {
            var categoriaCamiseta = new CategoriaViewModel
            {
                Codigo = 101,
                Nome = "Camisetas",
                Id = Guid.NewGuid()
            };
            var categoriaCaneca = new CategoriaViewModel
            {
                Codigo = 102,
                Nome = "Canecas",
                Id = Guid.NewGuid()
            };

            var camiseta1 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "camiseta1.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Camiseta Software Developer branca",
                Nome = "Camiseta Software Developer",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 10,
                Valor = 60M
            };

            var camiseta2 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "camiseta2.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Camiseta Software Developer branca",
                Nome = "Camiseta Software Developer",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 10,
                Valor = 60M
            };
            var camiseta3 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "camiseta3.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Camiseta Software Developer branca",
                Nome = "Camiseta Software Developer",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 10,
                Valor = 60M
            };
            var camiseta4 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "camiseta4.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Camiseta Software Developer branca",
                Nome = "Camiseta Software Developer",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 10,
                Valor = 60M
            };

            var caneca1 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "caneca1.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Caneca Starbugs branca",
                Nome = "Caneca Starbugs",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 5,
                Valor = 40M
            };
            var caneca2 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "caneca2.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Caneca Starbugs branca",
                Nome = "Caneca Starbugs",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 5,
                Valor = 40M
            };
            var caneca3 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "caneca3.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Caneca Starbugs branca",
                Nome = "Caneca Starbugs",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 5,
                Valor = 40M
            };
            var caneca4 = new ProdutoViewModel
            {
                CategoriaId = categoriaCamiseta.Id,
                Id = Guid.NewGuid(),
                Imagem = "caneca4.jpg",
                Altura = 1,
                Largura = 1,
                Profundidade = 1,
                Ativo = true,
                Descricao = "Caneca Starbugs branca",
                Nome = "Caneca Starbugs",
                DataCadastro = DateTime.Now,
                QuantidadeEstoque = 5,
                Valor = 40M
            };

            await _produtoAppService.AdicionarCategoria(categoriaCamiseta);
            await _produtoAppService.AdicionarCategoria(categoriaCaneca);
            await _produtoAppService.AdicionarProduto(camiseta1);
            await _produtoAppService.AdicionarProduto(camiseta2);
            await _produtoAppService.AdicionarProduto(camiseta3);
            await _produtoAppService.AdicionarProduto(camiseta4);
            await _produtoAppService.AdicionarProduto(caneca1);
            await _produtoAppService.AdicionarProduto(caneca2);
            await _produtoAppService.AdicionarProduto(caneca3);
            await _produtoAppService.AdicionarProduto(caneca4);

            return RedirectToAction("Index");
        }
        #endregion



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
            return View(produtoViewModel);
            
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
            
            return RedirectToAction("Index");
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
