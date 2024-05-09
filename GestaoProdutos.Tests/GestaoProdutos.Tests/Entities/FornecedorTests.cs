using GestaoProdutos.Domain.Entities;
using Xunit;

namespace GestaoProdutos.Tests.Entities
{
    public class FornecedorTests
    {
        [Fact]
        public void Fornecedor_DeveTerPropriedadesCorretas()
        {
            // Arrange
            var fornecedor = new Fornecedor();

            // Assert
            Assert.NotNull(fornecedor);
            Assert.Equal(default(int), fornecedor.Id);
            Assert.Null(fornecedor.Descricao);
            Assert.Null(fornecedor.CNPJ);
        }
    }
}
