


using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Catalogo.Infra.DataContext;
using NerdStore.Shared.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NerdStore.Catalogo.Infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly CatalogoContext _context;
        public ProdutoRepository(CatalogoContext catalogoContext)
        {
            _context = catalogoContext;
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int codigo)
        {
            return await _context.Produtos.AsNoTracking().Include(p => p.Categoria).Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);            
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

 
    }
}
