using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces;
using GestaoProdutos.Infrastructure.Context;
using GestaoProdutos.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Repositorios
{
    public class ProdutoRepositoryTests
    {
        private readonly DbContextOptions<GestaoProdutosContext> _dbContextOptions;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<GestaoProdutosContext>()
                .UseInMemoryDatabase(databaseName: "TesteRepository")
                .Options;
        }

        [Fact]
        public async Task Inserir_DeveInserirProdutoDoBanco()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions);
            var produto = ProdutoMock.RetornarProdutoMock("A");

            // Act
            await _produtoRepository.Inserir(produto);

            // Assert
            var produtoInserido = await dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.NotNull(produtoInserido);
            Assert.Equal(produto.Descricao, produtoInserido.Descricao);
            Assert.Equal(produto.Situacao, produtoInserido.Situacao);
        }

        [Fact]
        public async Task RecuperarPorId_DeveRetornarProdutoCorreto()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions);
            var produto = ProdutoMock.RetornarProdutoMock("A");
            await dbContext.Produtos.AddAsync(produto);
            await dbContext.SaveChangesAsync();

            // Act
            var produtoRecuperado = await _produtoRepository.RecuperarPorId(produto.Id);

            // Assert
            Assert.NotNull(produtoRecuperado);
            Assert.Equal(produto.Descricao, produtoRecuperado.Descricao);
            Assert.Equal(produto.Situacao, produtoRecuperado.Situacao);
        }

        [Fact]
        public async Task ListarComFiltroEPaginacao_DeveRetornarProdutosFiltradosComPaginacao()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions);
            var produto = ProdutoMock.RetornarProdutoMock("A");
            await InserirProdutosDeTeste(dbContext);
            var filtro = new ProdutoFiltro { Descricao = "Produto", Situacao = "A" };

            // Act
            var produtosPaginados = await _produtoRepository.ListarComFiltroEPaginacao(filtro, pagina: 1, quantidade: 10);

            // Assert
            Assert.NotNull(produtosPaginados);
            Assert.Equal(3, produtosPaginados.Count());
        }

        [Fact]
        public async Task AtualizarProduto_DeveAtualizarProdutoDoBanco()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions);
            var produto = ProdutoMock.RetornarProdutoMock("A");
            await dbContext.Produtos.AddAsync(produto);
            await dbContext.SaveChangesAsync();
            produto.Descricao = "Produto Atualizado";

            // Act
            await _produtoRepository.Atualizar(produto);

            // Assert
            var produtoAtualizado = await dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.NotNull(produtoAtualizado);
            Assert.Equal(produto.Descricao, produtoAtualizado.Descricao);
        }

        [Fact]
        public async Task ExcluirProduto_DeveExcluirProdutoDoBanco()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions);
            var produto = ProdutoMock.RetornarProdutoMock("A");
            await dbContext.Produtos.AddAsync(produto);
            await dbContext.SaveChangesAsync();

            // Act
            await _produtoRepository.Excluir(produto.Id);

            // Assert
            var produtoExcluido = await dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.Null(produtoExcluido);
        }

        private async Task InserirProdutosDeTeste(GestaoProdutosContext dbContext)
        {
            await dbContext.Produtos.AddRangeAsync(
                new Produto { Descricao = "Produto 1", Situacao = "A" },
                new Produto { Descricao = "Produto 2", Situacao = "A" },
                new Produto { Descricao = "Produto 3", Situacao = "I" }
            );
            await dbContext.SaveChangesAsync();
        }
    }
}
