using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Mocks
{
    public static class ProdutoMock
    {
        public static Produto RetornarProdutoMock(string situacao, Fornecedor fornecedor)
        {
            return new Produto { Descricao = "Produto de Teste", Situacao = situacao, DataFabricacao = System.DateTime.Now, DataValidade = System.DateTime.Now, FornecedorId = fornecedor.Id, Fornecedor = fornecedor };
        }
    }
}
