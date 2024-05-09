using FluentAssertions;
using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Tests.Builders;
using Xunit;

namespace GestaoProdutos.Tests.Entities
{
    public class FornecedorTests
    {
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
            var entity = new Fornecedor(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Descricao.Should().Be(dto.Descricao);
        }

        [Fact]
        public void DeveAtualizarFornecedor()
        {
            //arrange
            var entity = new FornecedorBuilder().Build();
            var dto = new FornecedorDto()
            {
                Cnpj = "12354",
                Descricao = "Descricao",
            };

            //action
            entity.Atualizar(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Descricao.Should().Be(dto.Descricao);
        }
    }
}
