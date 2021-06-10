
using NerdStore.Catalogo.Domain.Entidades.Validacoes;
using NerdStore.Shared.Entidades;
using System.Collections.Generic;

namespace NerdStore.Catalogo.Domain.Entidades
{
    public class Categoria : Entity 
    {
        protected Categoria() { }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar(this, new CategoriaValidator());
        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        // EF Relation
        public ICollection<Produto> Produtos { get; set; }

    }
}
