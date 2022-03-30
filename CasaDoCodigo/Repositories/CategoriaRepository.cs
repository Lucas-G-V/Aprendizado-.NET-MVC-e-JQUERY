using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task SaveCategorias(string categoria);
        Categoria FindCategoria(string categoria);

    }
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {

        }

        public Categoria FindCategoria(string categoria)
        {
            return dbSet.Where(fc => fc.Nome == categoria).FirstOrDefault();


        }

        public async Task SaveCategorias(string categoria)
        {


            if (!dbSet.Where(p => p.Nome == categoria).Any())
            {
                dbSet.Add(new Categoria(categoria));
            }

            await contexto.SaveChangesAsync();
        }


    }
}
