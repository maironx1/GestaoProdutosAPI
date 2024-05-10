using AutoMapper;
using FluentAssertions;
using GestaoProdutos.API.Controllers;
using GestaoProdutos.API.Models.Erro;
using GestaoProdutos.API.Models.Produto;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Application.Interfaces.Services;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Controllers
{
    public class ProdutoControllerTests : IDisposable
    {
        private readonly Mock<IProdutoService> _produtoServiceMock;
        private readonly IMapper _mapper;
        private readonly ProdutoController _produtoController;
        private readonly DbContextOptions<GestaoProdutosContext> _dbContextOptions;
        private readonly IConfiguration _configuration;

        public ProdutoControllerTests()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _mapper = Substitute.For<IMapper>();

            _dbContextOptions = new DbContextOptionsBuilder<GestaoProdutosContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContext = new GestaoProdutosContext(_dbContextOptions, _configuration);
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();

            _produtoController = new ProdutoController(_produtoServiceMock.Object, _mapper);
        }

        public void Dispose()
        {
            using (var context = new GestaoProdutosContext(_dbContextOptions, _configuration))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task InserirProduto_QuandoRequestNulo_ReturnsOkResult()
        {
            // Act
            var result = await _produtoController.InserirProduto(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var errorResponse = badRequestResult.Value as ErrorResponse;
            errorResponse.Should().NotBeNull();
            errorResponse.Codigo.Should().Be("Dados inválidos");
        }

        [Fact]
        public async Task InserirProduto_QuandoRequestValido_ReturnsOkResult()
        {
            // Arrange
            var produtoRequest = RetornarProdutoRequest();
            var produtoDto = RetornarProdutoDto();

            _mapper.Map<ProdutoDto>(produtoRequest).Returns(produtoDto);

            // Act
            var result = await _produtoController.InserirProduto(produtoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_QuandoRequestValido_ReturnsOkResult()
        {
            // Arrange
            var produtoId = 1;
            var produtoRequest = RetornarProdutoRequest();
            var produtoDto = RetornarProdutoDto();

            _mapper.Map<ProdutoDto>(produtoRequest).Returns(produtoDto);

            // Act
            var result = await _produtoController.AtualizarProduto(produtoId, produtoRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_IdValido_ReturnsOkResult()
        {
            // Arrange
            var produtoId = 1;

            // Act
            await _produtoController.RemoverProduto(produtoId);

            // Assert
            _produtoServiceMock.Verify(s => s.RemoverProduto(produtoId), Times.Once);
        }

        [Fact]
        public async Task ListarProdutosComFiltroEPaginacao_FiltroValido_ReturnsPaginatedProdutos()
        {
            //arrang
            var filter = new ProdutoFiltro();
            var produto = RetornarProdutoDto();

            var produtosPaginados = new PaginacaoDto<ProdutoDto>()
            {
                Items = new List<ProdutoDto>()
                {
                    produto
                },
                ItemsByPage = filter.ItemsByPage,
                PageIndex = filter.PageIndex,
                TotalItems = 10
            };

            _produtoServiceMock.Setup(s => s.ListarProdutosComFiltroEPaginacao(It.IsAny<ProdutoFiltro>())).ReturnsAsync(produtosPaginados);

            //action
            var response = await _produtoController.ListarProdutosComFiltroEPaginacao(filter);

            //assert
            response.TotalItems.Should().Be(produtosPaginados.TotalItems);
            response.Items.Should().HaveCount(1);
            response.PageIndex.Should().Be(produtosPaginados.PageIndex);
            response.ItemsByPage.Should().Be(produtosPaginados.ItemsByPage);
        }

        [Fact]
        public async Task RecuperarProdutoPorId_IdValido_ReturnsProduto()
        {
            //arrang
            long produtoId = 20;
            var produto = RetornarProdutoDto();
            var produtoResponse = new ProdutoResponse()
            {
                DataFabricacao = DateTime.Now,
                Descricao = "Description",
                DataValidade = DateTime.Now,
                Id = produtoId,
                FornecedorId = 1
            };

            _mapper.Map<ProdutoResponse>(produto).Returns(produtoResponse);

            _produtoServiceMock.Setup(s => s.RecuperarProdutoPorId(It.IsAny<long>())).ReturnsAsync(produto);

            //action
            var response = await _produtoController.RecuperarProdutoPorId(produtoId);

            //assert
            response.DataFabricacao.Should().Be(produtoResponse.DataFabricacao);
            response.DataValidade.Should().Be(produtoResponse.DataValidade);
        }

        [Fact]
        public async Task RecuperarProdutoPorId_IdInValido_ÑaoDeveReturnarProduto()
        {
            //arrang
            long produtoId = 20;

            //action
            var response = await _produtoController.RecuperarProdutoPorId(produtoId);

            //assert
            response.Should().BeNull();
        }

        private ProdutoRequest RetornarProdutoRequest()
        {
            return new ProdutoRequest
            {
                Descricao = "Produto de Teste",
                DataFabricacao = DateTime.Now.AddDays(-5),
                DataValidade = DateTime.Now.AddDays(30),
                FornecedorId = 1
            };
        }

        private ProdutoDto RetornarProdutoDto()
        {
            return new ProdutoDto
            {
                Id = 1,
                Descricao = "Produto de Teste",
                DataFabricacao = DateTime.Now.AddDays(-5),
                DataValidade = DateTime.Now.AddDays(30),
                FornecedorId = 1,
                Situacao = "A"
            };
        }
    }
}
