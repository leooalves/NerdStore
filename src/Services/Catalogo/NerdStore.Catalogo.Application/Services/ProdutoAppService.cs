
using AutoMapper;
using NerdStore.Catalogo.Application.ViewModel;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Catalogo.Domain.Service;
using NerdStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;

        public ProdutoAppService(IProdutoRepository produtoRepository, IEstoqueService estoqueService, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mapper = mapper;
        }

        public async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorCategoria(int codigo)
        {
            var produtos = await _produtoRepository.ObterProdutosPorCategoria(codigo);
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias()
        {
            var categorias = await _produtoRepository.ObterCategorias();
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias); ;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutos()
        {
            var produtos = await _produtoRepository.ObterTodosProdutos();
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        }


        public async Task<RespostaPadrao> AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            
            if (produto.EhInvalido)
                return new RespostaPadrao("Erro ao criar o produto", false, produto.ResultadoValidacao);

            _produtoRepository.Adicionar(produto);
            var resultado = await _produtoRepository.UnitOfWork.Commit();
            if (resultado)
                return new RespostaPadrao("Produto adicionado com sucesso", true);

            return new RespostaPadrao("Erro ao adicionar o produto", false);
        }

        public async Task<RespostaPadrao> AtualizarProduto(ProdutoViewModel produtoViewModel)
        {            
            var produtoAnterior = await _produtoRepository.ObterProdutoPorId(produtoViewModel.Id);
            if(produtoAnterior == null)
                return new RespostaPadrao("Produto não encontrado", true);

            produtoViewModel.QuantidadeEstoque = produtoAnterior.QuantidadeEstoque;

            var produto = _mapper.Map<Produto>(produtoViewModel);            

            if (produto.EhInvalido)
                return new RespostaPadrao("Erro ao atualizar o produto", false, produto.ResultadoValidacao);

            _produtoRepository.Atualizar(produto);

            var resultado = await _produtoRepository.UnitOfWork.Commit();
            if (resultado)
                return new RespostaPadrao("Produto atualizado com sucesso", true);

            return new RespostaPadrao("Erro ao atualizar o produto", false);
        }

        public async Task<RespostaPadrao> AdicionarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            if(categoria.EhInvalido)
                return new RespostaPadrao("Erro ao adicionar a categoria", false,categoria.ResultadoValidacao);

            _produtoRepository.Adicionar(categoria);

            var resultado = await _produtoRepository.UnitOfWork.Commit();
            if (resultado)
                return new RespostaPadrao("Categoria adicionada com sucesso", true);

            return new RespostaPadrao("Erro ao adicionar a categoria", false);

        }


        public async Task<RespostaPadrao> DebitarEstoqueProduto(Guid produtoId, int quantidade)
        {
            var resultado = await _estoqueService.DebitarEstoque(produtoId, quantidade);
            if (resultado)
                return new RespostaPadrao("Produto debitado do estoque com sucesso", true);

            return new RespostaPadrao("Não há produtos suficientes no estoque", false);
        }            

        public async Task<RespostaPadrao> ReporEstoqueProduto(Guid produtoId, int quantidade)
        {
            var resultado = await _estoqueService.ReporEstoque(produtoId, quantidade);
            if (resultado)
                return new RespostaPadrao("Reposição de produto realizada com sucesso", true);

            return new RespostaPadrao("Erro ao fazer a reposição do estoque", false);
        }


    }
}
