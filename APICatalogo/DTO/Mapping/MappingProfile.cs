using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTO.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<Categoria, CategoriaDTO>();
        }
    }
}
