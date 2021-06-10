
using NerdStore.Catalogo.Domain.Entidades.Validacoes;
using NerdStore.Shared.Entidades;

namespace NerdStore.Catalogo.Domain.Entidades
{
    public class Categoria : Entity 
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        protected Categoria() { }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public void Validar()
        {
            Validacoes = new CategoriaValidator().Validate(this);
        }

    }
}
