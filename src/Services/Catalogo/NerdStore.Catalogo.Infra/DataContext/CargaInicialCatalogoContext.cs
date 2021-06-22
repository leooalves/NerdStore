

using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Catalogo.Infra.DataContext
{
    public static class CargaInicialCatalogoContext
    {
        public static void Carregar(CatalogoContext catalogoContext)
        {
            catalogoContext.Database.Migrate();
            if (!catalogoContext.Categorias.Any())
            {
                var categorias = CargaInicialCategoria();
                catalogoContext.Categorias.AddRange(categorias);
                catalogoContext.SaveChanges();                          
            }
            if (!catalogoContext.Produtos.Any())
            {
                var produtos = CargaInicialProduto(catalogoContext);
                catalogoContext.Produtos.AddRange(produtos);
                catalogoContext.SaveChanges();
            }
                
            
        }

        private static IEnumerable<Categoria> CargaInicialCategoria()
        {
            return new List<Categoria>() {
                new Categoria("Camisetas",101),
                new Categoria("Canecas",102),
            };
        }

        private static IEnumerable<Produto> CargaInicialProduto(CatalogoContext catalogoContext)
        {
            IEnumerable<Categoria> categorias = catalogoContext.Categorias.ToList();
            return new List<Produto>()
            {
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta1.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta2.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta3.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Camiseta Software Developer", "Camiseta Software Developer Branca", true, 60M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Camisetas").Id, "camiseta4.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca1.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca2.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca3.jpg", new Dimensoes(1M, 1M, 1M)),
                new Produto("Caneca Starbugs", "Caneca Starbugs branca", true, 30M, DateTime.Now,
                            categorias.First(categoria => categoria.Nome=="Canecas").Id, "caneca4.jpg", new Dimensoes(1M, 1M, 1M)),
        };
        }
    }
}
