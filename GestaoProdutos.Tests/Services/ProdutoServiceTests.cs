using FluentAssertions;
using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using GestaoProdutos.Domain.Services;
using GestaoProdutos.Tests.Builders;
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

        public ProdutoServiceTests()
        {
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepository);
        }

        [Fact]
        public async Task DeveCriarProduto()
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
            await _produtoRepository.Received(1)
                .Inserir(Arg.Is<Produto>(x =>
                x.Descricao == dto.Descricao
                && x.DataFabricacao == dto.DataFabricacao
                && x.DataValidade == dto.DataValidade
                && x.Situacao == dto.Situacao
                && x.FornecedorId == dto.FornecedorId
                ));
        }

        [Fact]
        public async Task NaoDeveCriarProdutoPorDataFabricacaoMaiorDataValidade()
        {
            // arrange
            var dto = new ProdutoDto();

            // action
            var exception = await Record.ExceptionAsync(async () =>
            await _produtoService.InserirProduto(dto));

            //assert
            exception.Message.Should().Be("A data de fabricação deve ser anterior à data de validade.");
            await _produtoRepository.DidNotReceive().Inserir(Arg.Any<Produto>());
        }

        [Fact]
        public async Task DeveAtualizarProdutoAoExcluir()
        {
            //arrange
            var produto = new ProdutoBuilder().Build();
            _produtoRepository.RecuperarPorId(produto.Id).Returns(produto);

            //action
            await _produtoService.ExcluirProduto(produto.Id);

            //assert
            await _produtoRepository.Received(1).Atualizar(produto);
        }

        [Fact]
        public async Task NaoDeveAtualizarPorNaoAcharId()
        {
            //arrange
            long produtoId = 0;

            //action
            await _produtoService.ExcluirProduto(produtoId);

            //assert
            await _produtoRepository.DidNotReceive().Atualizar(Arg.Any<Produto>());
        }

        [Fact]
        public async Task DeveRetornarProdutosComPaginacao()
        {
            //arrange
            var filter = new ProdutoFiltro();
            var produto = new ProdutoBuilder().Build();

            var produtos = new PaginacaoDto<Produto>
            {
                ItemsByPage = 1,
                PageIndex = 1,
                TotalItems = 10,
                Items = new List<Produto>() { produto }
            };

            _produtoRepository.ListarComFiltroEPaginacao(filter).Returns(produtos);

            //action
            var response = await _produtoService.ListarProdutosComFiltroEPaginacao(filter);

            //assert
            response.Items.Should().HaveCount(1);
            response.ItemsByPage.Should().Be(produtos.ItemsByPage);
            response.PageIndex.Should().Be(produtos.PageIndex);
            response.TotalItems.Should().Be(produtos.TotalItems);
        }

        [Fact]
        public async Task DeveRetornarProdutoPorId()
        {
            //arrange
            var produto = new ProdutoBuilder().Build();
            _produtoRepository.RecuperarPorId(produto.Id).Returns(produto);

            //action
            var response = await _produtoService.RecuperarProdutoPorId(produto.Id);

            //assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task NaoDeveRetornarPorNaoAcharId()
        {
            //arrange
            long produtoId = 0;

            //action
            var response = await _produtoService.RecuperarProdutoPorId(produtoId);

            //assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task DeveAtualizarProduto()
        {
            //arrange
            var dto = new ProdutoDto()
            {
                DataFabricacao = DateTime.Now,
                Descricao = "Test",
                DataValidade = DateTime.Now.AddDays(1),
                FornecedorId = 1
            };

            var produto = new ProdutoBuilder().Build();
            _produtoRepository.RecuperarPorId(dto.Id).Returns(produto);

            //action
            await _produtoService.AtualizarProduto(dto);

            //assert
            await _produtoRepository.Received(1)
                .Atualizar(Arg.Is<Produto>(x =>
                x.Descricao == dto.Descricao
                && x.DataFabricacao == dto.DataFabricacao
                && x.DataValidade == dto.DataValidade
                && x.Situacao == dto.Situacao
                && x.FornecedorId == dto.FornecedorId
                ));
        }

        [Fact]
        public async Task NaoDeveAtualizarProdutoPorNaoAcharId()
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
            await _produtoService.AtualizarProduto(dto);

            //assert
            await _produtoRepository.DidNotReceive().Atualizar(Arg.Any<Produto>());
        }

        [Fact]
        public async Task NaoDeveAtualizarPorDataFabricacaoMaiorDataValidade()
        {
            // arrange
            var dto = new ProdutoDto();
            var produto = new ProdutoBuilder().Build();
            _produtoRepository.RecuperarPorId(dto.Id).Returns(produto);

            // action
            var exception = await Record.ExceptionAsync(async () =>
            await _produtoService.AtualizarProduto(dto));

            //assert
            exception.Message.Should().Be("A data de fabricação deve ser anterior à data de validade.");
            await _produtoRepository.DidNotReceive().Atualizar(Arg.Any<Produto>());
        }
    }
}
