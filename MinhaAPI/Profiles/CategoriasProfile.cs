using APICatalogo.DTO;
using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.Profiles;

public class CategoriasProfile : Profile
{
    public CategoriasProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}
