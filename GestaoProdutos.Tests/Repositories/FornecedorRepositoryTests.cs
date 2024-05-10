using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.Context;
using GestaoProdutos.Infrastructure.Repositories;
using GestaoProdutos.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Repositorios
{
    public class FornecedorRepositoryTests
    {
        private readonly DbContextOptions<GestaoProdutosContext> _dbContextOptions;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IConfiguration _configuration;

        public FornecedorRepositoryTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _dbContextOptions = new DbContextOptionsBuilder<GestaoProdutosContext>()
                .UseInMemoryDatabase(databaseName: "TesteRepository")
                .Options;

            _fornecedorRepository = new FornecedorRepository(new GestaoProdutosContext(_dbContextOptions, _configuration));
        }

        [Fact]
        public async Task Inserir_DeveInserirFornecedorDoBanco()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions, _configuration);
            var fornecedor = FornecedorMock.RetornarFornecedorMock("A");

            // Act
            await _fornecedorRepository.Inserir(fornecedor);

            // Assert
            var fornecedorInserido = await dbContext.Fornecedores.FirstOrDefaultAsync(p => p.Id == fornecedor.Id);
            Assert.NotNull(fornecedorInserido);
            Assert.Equal(fornecedor.Descricao, fornecedorInserido.Descricao);
            Assert.Equal(fornecedor.Situacao, fornecedorInserido.Situacao);
        }

        [Fact]
        public async Task RecuperarPorId_DeveRetornarFornecedorCorreto()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions, _configuration);
            var fornecedor = FornecedorMock.RetornarFornecedorMock("A");
            await dbContext.Fornecedores.AddAsync(fornecedor);
            await dbContext.SaveChangesAsync();

            // Act
            var fornecedorRecuperado = await _fornecedorRepository.RecuperarPorId(fornecedor.Id);

            // Assert
            Assert.NotNull(fornecedorRecuperado);
            Assert.Equal(fornecedor.Descricao, fornecedorRecuperado.Descricao);
            Assert.Equal(fornecedor.Situacao, fornecedorRecuperado.Situacao);
        }

        [Fact]
        public async Task AtualizarFornecedor_DeveAtualizarFornecedorDoBanco()
        {
            // Arrange
            using var dbContext = new GestaoProdutosContext(_dbContextOptions, _configuration);
            var fornecedor = FornecedorMock.RetornarFornecedorMock("A");
            await dbContext.Fornecedores.AddAsync(fornecedor);
            await dbContext.SaveChangesAsync();
            fornecedor.Descricao = "Fornecedor Atualizado";

            // Act
            await _fornecedorRepository.Atualizar(fornecedor);

            // Assert
            var fornecedorAtualizado = await dbContext.Fornecedores.FirstOrDefaultAsync(p => p.Id == fornecedor.Id);
            Assert.NotNull(fornecedorAtualizado);
            Assert.Equal(fornecedor.Descricao, fornecedorAtualizado.Descricao);
        }
    }
}
