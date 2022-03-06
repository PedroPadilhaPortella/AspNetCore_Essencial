using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }

        public PagedList<Produto> GetProducts(ProdutosParameters parameters)
        {
            //return GetAll()
            //    .OrderBy(p => p.Id)
            //    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            //    .Take(parameters.PageSize)
            //    .ToList();

            return PagedList<Produto>.ToPagedList(
                GetAll().OrderBy(p => p.Id),
                parameters.PageNumber,
                parameters.PageSize
            );
        }

        public IEnumerable<Produto> GetProductsByPrice()
        {
            return GetAll().OrderBy(p => p.Preco).ToList();
        }
    }
}
