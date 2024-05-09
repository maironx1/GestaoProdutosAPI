using FluentAssertions;
using GestaoProdutos.Domain.Validators;
using GestaoProdutos.Tests.Builders;
using System;
using System.Linq;
using Xunit;

namespace GestaoProdutos.Tests.Validator
{
    public class ProdutoValidatorTests
    {
        [Fact]
        public void DataFabricacaoDeveSerAnteriorDataValidade()
        {
            //arrange
            var product = new ProdutoBuilder()
                .ComDataFabricacao(DateTime.Now.AddDays(1))
                .ComDataValidade(DateTime.Now)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A data de fabricação deve ser anterior à data de validade.");
        }

        [Fact]
        public void DescricaoNaoInformado()
        {
            //arrange
            var product = new ProdutoBuilder()
                .ComDescricao(null)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A descrição do produto deve ser informado.");
        }

        [Fact]
        public void FornecedorInvalido()
        {
            //arrange
            var product = new ProdutoBuilder()
                .ComFornecedorId(0)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("Deve ser informado um fornecedor valido!");
        }

        [Fact]
        public void ProdutoVazio()
        {
            //arrange
            var product = new ProdutoBuilder()
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().BeEmpty();
        }
    }
}
