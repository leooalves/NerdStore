
using FluentValidation;
using FluentValidation.Results;

namespace NerdStore.Shared.Entidades
{
    public abstract class Validavel
    {        
        public ValidationResult ResultadoValidacao { get; protected set; }

        public bool EhValida => ResultadoValidacao.IsValid;

        public bool EhInvalida => !EhValida;

        public void Validar<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ResultadoValidacao = validator.Validate(model);            
        }

    }
}
