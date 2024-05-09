using GestaoProdutos.Domain.Entities;
using Xunit;

namespace GestaoProdutos.Tests.Entidades
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_DeveTerPropriedadesCorretas()
        {
            // Arrange
            var produto = new Produto();

            // Assert
            Assert.NotNull(produto);
            Assert.Equal(default(int), produto.Id);
            Assert.Null(produto.Descricao);
            Assert.Null(produto.Situacao);
            Assert.Null(produto.DataFabricacao);
            Assert.Null(produto.DataValidade);
            Assert.Null(produto.FornecedorId);
            Assert.Null(produto.Fornecedor);
        }
    }
}
