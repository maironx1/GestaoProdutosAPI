using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.Context;
using GestaoProdutos.Infrastructure.Repositories;
using GestaoProdutos.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Repositorios
{
    public class ProdutoRepositoryTests
    {
        private readonly DbContextOptions<GestaoProdutosContext> _dbContextOptions;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IConfiguration _configuration;
        private readonly GestaoProdutosContext _context;

        public ProdutoRepositoryTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _dbContextOptions = new DbContextOptionsBuilder<GestaoProdutosContext>()
                .UseInMemoryDatabase(databaseName: "TesteRepository")
                .Options;

            _context = new GestaoProdutosContext(_dbContextOptions, _configuration);

            _produtoRepository = new ProdutoRepository(_context);
        }

        [Fact]
        public async Task Inserir_DeveInserirProdutoDoBanco()
        {
            // Arrange
            var fornecedorMock = new Fornecedor { Cnpj = "123456", Id = 2 };
            var produto = ProdutoMock.RetornarProdutoMock("A", fornecedorMock);

            // Act
            await _produtoRepository.Inserir(produto);

            // Assert
            var produtoInserido = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.NotNull(produtoInserido);
            Assert.Equal(produto.Descricao, produtoInserido.Descricao);
            Assert.Equal(produto.Situacao, produtoInserido.Situacao);
        }

        [Fact]
        public async Task RecuperarPorId_DeveRetornarProdutoCorreto()
        {
            // Arrange
            var fornecedorMock = new Fornecedor { Cnpj = "123456", Id = 3 };
            var produto = ProdutoMock.RetornarProdutoMock("A", fornecedorMock);
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

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
            await InserirProdutosDeTeste();
            var filtro = new ProdutoFiltro { Descricao = "Produto", Situacao = "A", ItemsByPage = 2 };

            // Act
            var produtosPaginados = await _produtoRepository.ListarComFiltroEPaginacao(filtro);

            // Assert
            Assert.NotNull(produtosPaginados);
            Assert.Equal(2, produtosPaginados.Items.Count());
        }

        [Fact]
        public async Task AtualizarProduto_DeveAtualizarProdutoDoBanco()
        {
            // Arrange
            var fornecedorMock = new Fornecedor { Cnpj = "123456", Id = 7 };
            var produto = ProdutoMock.RetornarProdutoMock("A", fornecedorMock);
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            produto.Descricao = "Produto Atualizado";

            // Act
            await _produtoRepository.Atualizar(produto);

            // Assert
            var produtoAtualizado = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.NotNull(produtoAtualizado);
            Assert.Equal(produto.Descricao, produtoAtualizado.Descricao);
        }

        private async Task InserirProdutosDeTeste()
        {
            _context.Produtos.AddRange(
                new Produto { Descricao = "Produto 1", Situacao = "A", DataFabricacao = System.DateTime.Now, DataValidade = System.DateTime.Now, FornecedorId = 10, Fornecedor = new Fornecedor { Cnpj = "123456", Id = 10 } },
                new Produto { Descricao = "Produto 2", Situacao = "A", DataFabricacao = System.DateTime.Now, DataValidade = System.DateTime.Now, FornecedorId = 11, Fornecedor = new Fornecedor { Cnpj = "123457", Id = 11 } },
                new Produto { Descricao = "Produto 3", Situacao = "I", DataFabricacao = System.DateTime.Now, DataValidade = System.DateTime.Now, FornecedorId = 12, Fornecedor = new Fornecedor { Cnpj = "123458", Id = 12 } }
            );
            await _context.SaveChangesAsync();
        }
    }
}
