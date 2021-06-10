

using FluentValidation;
using System;

namespace NerdStore.Catalogo.Domain.Entidades.Validacoes
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {            
            RuleFor(produto => produto.Descricao).NotEmpty().WithMessage("A {PropertyName} não pode ser vazio");            
            RuleFor(produto => produto.Nome).NotEmpty().WithMessage("O {PropertyName} não pode ser vazio");            
            RuleFor(produto => produto.Imagem).NotEmpty().WithMessage("A {PropertyName} não pode ser vazio");
            RuleFor(produto => produto.Valor).GreaterThan(0).WithMessage("O {PropertyName} deve ser maior do que zero");
            RuleFor(produto => produto.CategoriaId).NotEmpty().WithMessage("O {PropertyName} não pode ser vazio");

        }
    }
}
