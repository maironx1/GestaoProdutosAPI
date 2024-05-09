using AutoMapper;
using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Fornecedor, FornecedorDto>().ReverseMap();
        }
    }
}
