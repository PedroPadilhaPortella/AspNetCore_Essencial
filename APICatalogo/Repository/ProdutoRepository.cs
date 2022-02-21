using APICatalogo.Context;
using APICatalogo.Models;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Produto> GetProductsByPrice()
        {
            return GetAll().OrderBy(p => p.Preco).ToList();
        }
    }
}
