using AutoMapper;
using FluentAssertions;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Validators;
using GestaoProdutos.Tests.Builders;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace GestaoProdutos.Tests.Validator
{
    public class ProdutoValidatorTests
    {
        private readonly IMapper _mapper;

        public ProdutoValidatorTests()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(new Produto
                {
                    DataFabricacao = DateTime.Now.Date,
                    DataValidade = DateTime.Now.Date.AddDays(1),
                    Descricao = "teste",
                    FornecedorId = 1,
                    Situacao = "A"
                });

            _mapper = mapperMock.Object;
        }

        [Fact]
        public void DataFabricacao_QuandoMaiorQueDataValidade_DeveRetornarMensagemErro()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();
            produto.DataFabricacao = DateTime.Now.Date.AddDays(1);
            produto.DataValidade = DateTime.Now.Date;

            var validator = new ProdutoValidator();

            //action
            var response = validator.Validate(produto);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A data de fabricação deve ser anterior à data de validade.");
        }

        [Fact]
        public void Descricao_QuandoNaoInformado_DeveRetornarMensagemErro()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();
            produto.Descricao = null;

            var validator = new ProdutoValidator();

            //action
            var response = validator.Validate(produto);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A descrição do produto deve ser informado.");
        }

        [Fact]
        public void Fornecedor_QuandoInvalido_DeveRetornarMensagemErro()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();
            produto.FornecedorId = 0;

            var validator = new ProdutoValidator();

            //action
            var response = validator.Validate(produto);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("Deve ser informado um fornecedor valido!");
        }

        [Fact]
        public void Produto_QuandoVazio_DeveRetornarMensagemErro()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();

            var validator = new ProdutoValidator();

            //action
            var response = validator.Validate(produto);

            //assert
            response.Errors.Should().BeEmpty();
        }
    }
}
