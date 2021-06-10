
using FluentValidation.Results;

namespace NerdStore.Shared.Entidades
{
    public abstract class Validavel
    {        
        public ValidationResult Validacoes { get; set; }

        public bool EhValida
        {
            get
            {
                return Validacoes.IsValid;
            }
        }
        public bool EhInvalida
        {
            get
            {
                return !Validacoes.IsValid;
            }
        }
    }
}
