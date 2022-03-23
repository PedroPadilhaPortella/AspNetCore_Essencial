using APICatalogo.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.GraphQL
{
    public class CategoriaType : ObjectGraphType<Categoria>
    {
        //Definindo qual entidade será mapeada pra o Type
        public CategoriaType()
        {
            Field(x => x.Id); 
            Field(x => x.Nome);
            Field(x => x.ImagemUrl);

            Field<ListGraphType<CategoriaType>>("categorias");   
        }
    }
}
