using AutoMapper;
using GestaoProdutos.API.Models.Fornecedor;
using GestaoProdutos.API.Models.Produto;
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

            CreateMap<FornecedorRequest, FornecedorDto>();
            CreateMap<FornecedorDto, FornecedorResponse>();

            CreateMap<ProdutoRequest, ProdutoDto>();
            CreateMap<ProdutoDto, ProdutoResponse>();
        }
    }
}
