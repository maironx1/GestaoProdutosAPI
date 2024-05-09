using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Mocks
{
    public static class ProdutoMock
    {
        public static Produto RetornarProdutoMock(string situacao)
        {
            return new Produto { Descricao = "Produto de Teste", Situacao = situacao };
        }
    }
}
