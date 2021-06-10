using FluentValidation;

namespace NerdStore.Catalogo.Domain.Entidades.Validacoes
{
    public class DimensoesValidator : AbstractValidator<Dimensoes>
    {
        public DimensoesValidator()
        {
            RuleFor(dimensoes => dimensoes.Altura).GreaterThan(0).WithMessage("A {PropertyName} deve ser maior do que zero.");
            RuleFor(dimensoes => dimensoes.Largura).GreaterThan(0).WithMessage("A {PropertyName} deve ser maior do que zero."); 
            RuleFor(dimensoes => dimensoes.Profundidade).GreaterThan(0).WithMessage("A {PropertyName} deve ser maior do que zero.");
        }
    }
}
