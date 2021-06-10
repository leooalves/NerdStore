

using FluentValidation;

namespace NerdStore.Catalogo.Domain.Entidades.Validacoes
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(categoria => categoria.Nome).NotEmpty().WithMessage("O {PropertyName} não deve ser vazio.");
            RuleFor(categoria => categoria.Codigo).GreaterThan(0).WithMessage("O {PropertyName} deve ser maior do que zero.");
        }
    }
}
