using AutoMapper;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<Fornecedor, FornecedorDto>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap(typeof(Paginacao<>), typeof(PaginacaoDto<>));
            CreateMap(typeof(PaginacaoDto<>), typeof(Paginacao<>));
        }
    }
}
