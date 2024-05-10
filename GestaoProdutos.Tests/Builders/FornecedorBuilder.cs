using AutoMapper;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Builders
{
    public class FornecedorBuilder
    {
        private readonly string _descricao = "descricao";
        private readonly string _cnpj = "TesteCnpj";

        private readonly IMapper _mapper;

        public FornecedorBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Fornecedor Build()
        {
            var fornecedordto = new FornecedorDto()
            {
                Id = 1,
                Cnpj = _cnpj,
                Descricao = _descricao
            };

            return _mapper.Map<Fornecedor>(fornecedordto);
        }
    }
}
