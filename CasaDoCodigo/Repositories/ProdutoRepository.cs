using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }
        public IList<Produto> GetProdutos()
        {
            return dbSet.Include(p => p.Categoria)
                .ToList();
        }
        public IList<IGrouping<Categoria, Produto>> GetCategoriaProdutos()
        {
            return dbSet.Include(p => p.Categoria).GroupBy(x => x.Categoria)
                .ToList();
        }
        public async Task<IList<IGrouping<Categoria, Produto>>> GetCategoriaProdutos(string pesquisa)
        {

            List<IGrouping<Categoria, Produto>> produtoscategoria = await dbSet
                
                .Include(p => p.Categoria)
                .GroupBy(x => x.Categoria)
                .Where(p => p.Key.Nome.Contains(pesquisa))
                .ToListAsync();

            List<IGrouping<Categoria, Produto>> produtos = await dbSet
                .Where(p =>p.Nome.Contains(pesquisa))
                .Include(p => p.Categoria)
                .GroupBy(x => x.Categoria)
                .ToListAsync();
            produtos.Concat(produtoscategoria);
            
            return produtos;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {

                    await categoriaRepository.SaveCategorias(livro.Categoria);
                    Categoria categoria = categoriaRepository.FindCategoria(livro.Categoria);

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
