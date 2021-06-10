

using FluentValidation;

namespace NerdStore.Catalogo.Domain.Entidades.Validacoes
{
    public class ProdutoDebitarEstoqueValidator: AbstractValidator<Produto>
    {
        public ProdutoDebitarEstoqueValidator()
        {
            RuleFor(produto => produto.QuantidadeEstoque).GreaterThanOrEqualTo(0).WithMessage("Estoque insuficiente");
        }
    }
}
