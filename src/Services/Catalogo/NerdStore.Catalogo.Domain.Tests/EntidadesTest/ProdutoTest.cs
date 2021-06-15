using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Entidades.Validacoes;
using FluentValidation.TestHelper;
using System;

namespace NerdStore.Catalogo.Domain.Tests.Entidades
{
    [TestClass]
    public class ProdutoTest
    {
        private readonly ProdutoValidator _validator;

        public ProdutoTest()
        {
            _validator = new ProdutoValidator();
        }

        [TestMethod]
        public void Dado_produto_sem_nome_deve_dar_erro()
        {
            var produto = new Produto(string.Empty, "Descricao", true, 10,DateTime.Now ,Guid.NewGuid(), "imagem", new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor("Nome");
        }

        [TestMethod]
        public void Dado_produto_sem_descricao_deve_dar_erro()
        {
            var produto = new Produto("Nome", string.Empty, true, 10, DateTime.Now, Guid.NewGuid(), "imagem", new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor("Descricao");
        }

        [TestMethod]
        public void Dado_produto_sem_imagem_deve_dar_erro()
        {
            var produto = new Produto("Nome", "descricao", true, 10, DateTime.Now, Guid.NewGuid(), string.Empty, new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor("Imagem");
        }

        [TestMethod]
        public void Dado_produto_sem_categoria_deve_dar_erro()
        {
            var produto = new Produto("Nome", "descricao", true, 10, DateTime.Now, Guid.Empty, string.Empty, new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor("CategoriaId");
        }

        [TestMethod]
        public void Dado_produto_com_valor_menor_que_zero_deve_dar_erro()
        {
            var produto = new Produto("Nome", "descricao", true, -1, DateTime.Now, Guid.Empty, string.Empty, new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor("Valor");
        }

        [TestMethod]
        public void Dado_produto_com_dimensao_invalida_deve_dar_erro()
        {
            var produto = new Produto("Nome", "descricao", true, 0, DateTime.Now, Guid.NewGuid(), "Imagem", new Dimensoes(0, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Dimensoes.Altura);
        }

        [TestMethod]
        public void Dado_produto_valido_nao_deve_dar_erro()
        {
            var produto = new Produto("Nome", "descricao", true, 0, DateTime.Now, Guid.NewGuid(), "imagem", new Dimensoes(1, 1, 1));
            var resultado = _validator.TestValidate(produto);
            resultado.ShouldNotHaveAnyValidationErrors();
        }

    }
}
