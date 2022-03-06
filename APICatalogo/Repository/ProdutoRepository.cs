using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }

        public async Task<PagedList<Produto>> GetProducts(ProdutosParameters parameters)
        {
            //return GetAll()
            //    .OrderBy(p => p.Id)
            //    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            //    .Take(parameters.PageSize)
            //    .ToList();

            return await PagedList<Produto>.ToPagedList(
                GetAll().OrderBy(p => p.Id),
                parameters.PageNumber,
                parameters.PageSize
            );
        }

        public async Task<IEnumerable<Produto>> GetProductsByPrice()
        {
            return await GetAll().OrderBy(p => p.Preco).ToListAsync();
        }
    }
}
