using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        Task SaveProdutos(List<Livro> livros);
        IList<Produto> GetProdutos();
        IList<IGrouping<Categoria, Produto>> GetCategoriaProdutos();
        Task<IList<IGrouping<Categoria, Produto>>> GetCategoriaProdutos(string pesquisa);
    }
}