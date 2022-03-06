using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategories(CategoriasParameters parameters);
        IEnumerable<Categoria> GetCategoriesProducts();
    }
}
