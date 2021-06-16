

using NerdStore.Catalogo.Domain.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Catalogo.Infra.DataContext
{
    public static class CargaInicialCatalogoContext
    {
        public static void Carregar(CatalogoContext catalogoContext)
        {
            if (!catalogoContext.Categorias.Any())
            {
                var categorias = CargaInicialCategoria();
                catalogoContext.Categorias.AddRange(categorias);
                catalogoContext.Produtos.AddRange(CargaInicialProduto(categorias));
                catalogoContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Categoria> CargaInicialCategoria()
        {
            return new List<Categoria>() {
                new Categoria("Camisetas",101),
                new Categoria("Canecas",102),
            };
        }

        private static IEnumerable<Produto> CargaInicialProduto(IEnumerable<Categoria> categorias)
        {
            var dimensao = new Dimensoes(1, 1, 1);

            return new List<Produto>()
            {
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta1.jpg", dimensao),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta2.jpg", dimensao),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta3.jpg", dimensao),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta4.jpg", dimensao),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca1.jpg", dimensao),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca2.jpg", dimensao),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca3.jpg", dimensao),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca4.jpg", dimensao),
        };
        }
    }
}
