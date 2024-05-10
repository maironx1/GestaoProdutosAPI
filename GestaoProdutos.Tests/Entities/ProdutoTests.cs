using AutoMapper;
using FluentAssertions;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Tests.Builders;
using Moq;
using NSubstitute;
using System;
using Xunit;

namespace GestaoProdutos.Tests.Entities
{
    public class ProdutoTests
    {
        private readonly IMapper _mapper;

        public ProdutoTests()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(new Produto {
                    DataFabricacao = DateTime.Now.Date,
                    DataValidade = DateTime.Now.Date.AddDays(1),
                    Descricao = "teste",
                    FornecedorId = 1,
                    Situacao = "A"
                });

            _mapper = mapperMock.Object;
        }

        [Fact]
        public void DeveCriarProdutoValido()
        {
            //arrange
            long fornecedorId = 1;

            var dto = new ProdutoDto
            {
                DataFabricacao = DateTime.Now.Date,
                DataValidade = DateTime.Now.Date.AddDays(1),
                Descricao = "teste",
                FornecedorId = fornecedorId,
                Situacao = "A"
            };

            //action
            var produto = _mapper.Map<Produto>(dto);

            //assert
            produto.Situacao.Should().Be(dto.Situacao);
            produto.Descricao.Should().Be(dto.Descricao);
            produto.DataFabricacao.Should().Be(dto.DataFabricacao);
            produto.DataValidade.Should().Be(dto.DataValidade);
            produto.FornecedorId.Should().Be(fornecedorId);
        }

        [Fact]
        public void DeveDesativarProduto()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();

            //action
            produto.Desativar();

            //assert
            produto.Situacao.Should().Be("I");
        }

        [Fact]
        public void DeveAtivarProduto()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();

            //action
            produto.Ativar();

            //assert
            produto.Situacao.Should().Be("A");
        }

        [Fact]
        public void DeveRetornarErroDataFabricacaoSuperiorDataValidade()
        {
            //arrange
            var produto = new ProdutoBuilder(_mapper)
                .Build();
            produto.DataFabricacao = DateTime.Now.Date.AddDays(1);
            produto.DataValidade = DateTime.Now.Date;

            //action
            var response = produto.IsValid();

            //assert
            response.Errors.Should().Contain(x => x.ErrorMessage == "A data de fabricação deve ser anterior à data de validade.");
        }
    }
}
