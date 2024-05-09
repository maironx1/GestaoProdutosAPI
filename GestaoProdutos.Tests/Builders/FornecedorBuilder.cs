using AutoMapper;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Builders
{
    public class FornecedorBuilder
    {
        private readonly string _descricao = "descricao";
        private readonly string _cnpj = "cnpj";

        private readonly IMapper _mapper;

        public FornecedorBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Fornecedor Build()
        {
            var fornecedordto = new FornecedorDto()
            {
                Cnpj = _cnpj,
                Descricao = _descricao
            };

            return _mapper.Map<Fornecedor>(fornecedordto);
        }
    }
}
