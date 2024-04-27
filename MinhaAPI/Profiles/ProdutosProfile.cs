using APICatalogo.DTO;
using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.Profiles;

public class ProdutosProfile : Profile
{
    public ProdutosProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoEditarRequest>().ReverseMap();
        CreateMap<Produto, ProdutoEditarResponse>().ReverseMap();
    }
}
