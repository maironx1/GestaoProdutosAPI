using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Mocks
{
    public static class FornecedorMock
    {
        public static Fornecedor RetornarFornecedorMock(string situacao)
        {
            return new Fornecedor { Descricao = "Fornecedor de Teste", Situacao = situacao, Cnpj = "Teste Cnpj" };
        }
    }
}
