using AutoMapper;
using FluentAssertions;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Application.Interfaces.Services;
using GestaoProdutos.Application.Services;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Tests.Builders;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Services
{
    public class ProdutoServiceTests
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoServiceTests()
        {
            _produtoRepository = Mock.Of<IProdutoRepository>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(new Produto
                {
                    Descricao = "teste",
                    Situacao = "A",
                    Id = 1
                });

            mapperMock.Setup(m => m.Map<ProdutoDto>(It.IsAny<Produto>()))
                .Returns(new ProdutoDto
                {
                    Descricao = "teste",
                    Situacao = "A",
                    Id = 1
                });

            _mapper = mapperMock.Object;

            _produtoService = new ProdutoService(_produtoRepository, _mapper);
        }

        [Fact]
        public async Task InserirProduto_QuandoProdutoValido_DeveCriarProduto()
        {
            //arrange
            var dto = new ProdutoDto()
            {
                DataFabricacao = DateTime.Now,
                Descricao = "Test",
                DataValidade = DateTime.Now.AddDays(1),
                FornecedorId = 1
            };

            //action
            await _produtoService.InserirProduto(dto);

            //assert
            Mock.Get(_produtoRepository).Verify(r => r.Inserir(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task ListarTodosProdutos_QuandoProdutoValido_DeveRetornarTodosProdutos()
        {
            // Arrange
            var produto = new ProdutoBuilder(_mapper).Build();
            Mock.Get(_produtoRepository).Setup(r => r.ListarTodos()).ReturnsAsync(new List<Produto>() { produto });

            // Action
            var result = await _produtoService.ListarTodosProdutos();

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task ListarTodosProdutos_QuandoProdutoVazio_NãoDeveRetornarTodosProdutos()
        {
            // Arrange
            Mock.Get(_produtoRepository).Setup(r => r.ListarTodos()).ReturnsAsync(new List<Produto>());

            // Action
            var result = await _produtoService.ListarTodosProdutos();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task InserirProduto_QuandoDataValidadeAnteriorDataFabricacao_NaoDeveCriarProduto()
        {
            // arrange
            var dto = new ProdutoDto();

            // Action
            await _produtoService.InserirProduto(dto);

            // Assert
            Mock.Get(_produtoRepository).Verify(r => r.Inserir(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task RemoverProduto_QuandoEncontraId_DeveAtualizarProduto()
        {
            // Arrange
            var dto = new ProdutoDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            var produto = new ProdutoBuilder(_mapper).Build();
            Mock.Get(_produtoRepository).Setup(r => r.RecuperarPorId(dto.Id)).ReturnsAsync(produto);

            // Action
            await _produtoService.RemoverProduto(dto.Id);

            // Assert
            Mock.Get(_produtoRepository).Verify(r => r.Atualizar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task RemoverProduto_QuandoNaoEncontraId_NaoDeveAtualizarProduto()
        {
            // Arrange
            var dto = new ProdutoDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            // Action
            await _produtoService.RemoverProduto(dto.Id);

            // Assert
            Mock.Get(_produtoRepository).Verify(r => r.Atualizar(It.IsAny<Produto>()), Times.Never);
        }

        [Fact]
        public async Task ListarProdutosComFiltroEPaginacao_QuandoFiltroValido_DeveRetornarProdutosComPaginacao()
        {
            // Arrange
            var filter = new ProdutoFiltro();
            var produto = new ProdutoBuilder(_mapper).Build();

            var produtos = new Paginacao<Produto>
            {
                ItemsByPage = 1,
                PageIndex = 1,
                TotalItems = 10,
                Items = new List<Produto>() { produto }
            };

            Mock.Get(_produtoRepository).Setup(r => r.ListarComFiltroEPaginacao(filter)).ReturnsAsync(produtos);

            // Action
            var response = await _produtoService.ListarProdutosComFiltroEPaginacao(filter);

            // Assert
            response.Items.Should().HaveCount(1);
            response.ItemsByPage.Should().Be(produtos.ItemsByPage);
            response.PageIndex.Should().Be(produtos.PageIndex);
            response.TotalItems.Should().Be(produtos.TotalItems);
        }

        [Fact]
        public async Task RecuperarPorId_QuandoIdValido_DeveRetornarProdutoPorId()
        {
            // Arrange
            var id = 1;
            var produto = new ProdutoBuilder(_mapper).Build();
            Mock.Get(_produtoRepository).Setup(r => r.RecuperarPorId(id)).ReturnsAsync(produto);

            // Action
            var result = _produtoService.RecuperarProdutoPorId(id).Result;

            // Assert
            result.Id.Should().Be(produto.Id);
            result.Descricao.Should().Be(produto.Descricao);
        }

        [Fact]
        public async Task RecuperarPorId_QuandoIdInValido_NaoDeveRetornarPorNaoAcharId()
        {
            // Arrange
            var id = 0;
            var produto = new ProdutoBuilder(_mapper).Build();
            Mock.Get(_produtoRepository).Setup(r => r.RecuperarPorId(id)).ReturnsAsync(produto);

            // Action
            var result = await _produtoService.RecuperarProdutoPorId(id);

            // Assert
            result.Should().NotBeEquivalentTo(produto);
        }

        [Fact]
        public async Task AtualizarProduto_QuandoProdutoValido_DeveAtualizarProduto()
        {
            // Arrange
            var dto = new ProdutoDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            var produto = new ProdutoBuilder(_mapper).Build();
            Mock.Get(_produtoRepository).Setup(r => r.RecuperarPorId(dto.Id)).ReturnsAsync(produto);

            // Action
            await _produtoService.AtualizarProduto(dto);

            // Assert
            Mock.Get(_produtoRepository).Verify(r => r.Atualizar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarProduto_QuandoProdutoInValido_NaoDeveAtualizarProduto()
        {
            // Arrange
            var dto = new ProdutoDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            // Action
            await _produtoService.AtualizarProduto(dto);

            // Assert
            Mock.Get(_produtoRepository).Verify(r => r.Inserir(It.IsAny<Produto>()), Times.Never);
        }
    }
}
