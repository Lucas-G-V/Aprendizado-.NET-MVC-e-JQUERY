using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class ProdutosPesquisaViewModel
    {
        public  ProdutosPesquisaViewModel(Produto produto, string pesquisa)
        {
            Produto = produto;
            Pesquisa = pesquisa;
        }

        public Produto Produto { get; }
        public string Pesquisa { get; set; }
    }
}
