using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> LocatePage<Tipo>(int pagina, int tamanho) where Tipo : class;
        int GetTotalDeRegistros();
    }
}
