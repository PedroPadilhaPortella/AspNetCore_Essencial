using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Categoria> GetCategoriesProducts()
        {
            return GetAll().Include(c => c.Produtos);
        }

        public PagedList<Categoria> GetCategories(CategoriasParameters parameters)
        {
            return PagedList<Categoria>.ToPagedList(
                GetAll().OrderBy(c => c.Id),
                parameters.PageNumber,
                parameters.PageSize
            );
        }
    }
}
