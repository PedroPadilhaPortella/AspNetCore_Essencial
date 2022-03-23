using APICatalogo.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.GraphQL
{
    // Mapeando os dados para uma dada consulta, chamando nosso repository
    // CategoriasRepository
    public class CategoriaQuery : ObjectGraphType
    {
        public CategoriaQuery(IUnitOfWork _context)
        {
            Field<CategoriaType>("categoria", 
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>() { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return _context.CategoriaRepository.GetById(c => c.Id == id);
                });

            Field<ListGraphType<CategoriaType>>("categorias",
                resolve: context =>
                {
                    return _context.CategoriaRepository.GetAll();
                });
        }
    }
}
