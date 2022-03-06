using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProducts(ProdutosParameters parameters);
        IEnumerable<Produto> GetProductsByPrice();
    }
}
