using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Categoria>> GetCategoriesProducts()
        {
            return await GetAll().Include(c => c.Produtos).ToListAsync();
        }

        public async Task<PagedList<Categoria>> GetCategories(CategoriasParameters parameters)
        {
            return await PagedList<Categoria>.ToPagedList(
                GetAll().OrderBy(c => c.Id),
                parameters.PageNumber,
                parameters.PageSize
            );
        }
    }
}
