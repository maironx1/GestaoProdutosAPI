using AutoMapper;
using FluentAssertions;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Tests.Builders;
using Xunit;

namespace GestaoProdutos.Tests.Entities
{
    public class FornecedorTests
    {
        private readonly IMapper _mapper;

        public FornecedorTests(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public void DeveCriarFornecedor()
        {
            //arrange
            var dto = new FornecedorDto()
            {
                Cnpj = "12354",
                Descricao = "Descricao",
            };

            //action
            var entity = _mapper.Map<Fornecedor>(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Descricao.Should().Be(dto.Descricao);
        }

        [Fact]
        public void DeveAtualizarFornecedor()
        {
            //arrange
            var entity = new FornecedorBuilder(_mapper).Build();
            var dto = new FornecedorDto()
            {
                Cnpj = "12354",
                Descricao = "Descricao",
            };

            //action
            entity = _mapper.Map<Fornecedor>(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Descricao.Should().Be(dto.Descricao);
        }
    }
}
