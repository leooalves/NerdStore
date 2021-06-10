

using NerdStore.Catalogo.Domain.Entidades.Validacoes;
using NerdStore.Shared.Entidades;

namespace NerdStore.Catalogo.Domain.Entidades
{
    public class Dimensoes : ValueObject
    {
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;

            Validar(this, new DimensoesValidator());
        }

        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }        
     
        public string DescricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public override string ToString()
        {
            return DescricaoFormatada();
        }

     
    }
}
