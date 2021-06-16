

using NerdStore.Catalogo.Domain.Entidades.Validacoes;
using NerdStore.Shared.Entidades;
using System;


namespace NerdStore.Catalogo.Domain.Entidades
{
    public class Produto : Entity, IAggregateRoot
    {
        protected Produto() { }
        public Produto(string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro , Guid categoriaId, string imagem, Dimensoes dimensoes)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            Dimensoes = dimensoes;
            QuantidadeMinimaReporEstoque = 3;

            Validar();
        }

        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public int QuantidadeMinimaReporEstoque{ get; private set; }

        public Dimensoes Dimensoes { get; private set; }
        public Categoria Categoria { get; private set; }   

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria)
        {
            if (categoria.EhValido)
            {
                Categoria = categoria;
                CategoriaId = categoria.Id;
            }
            else
            {
                ResultadoValidacao = categoria.ResultadoValidacao;
            }                        
        }

        public void AlterarDescricao(string descricao)
        {            
            Descricao = descricao;
            Validar();
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;            
            QuantidadeEstoque -= quantidade;
            Validar(this, new ProdutoDebitarEstoqueValidator());
        }

        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public void Validar()
        {
            Validar(this, new ProdutoValidator());
        }

       
    }
}
