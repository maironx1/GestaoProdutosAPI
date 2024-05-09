using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Tests.Builders
{
    public class FornecedorBuilder
    {
        private readonly string _descricao = "descricao";
        private readonly string _cnpj = "cnpj";

        public Fornecedor Build()
        {
            var fornecedordto = new FornecedorDto()
            {
                Cnpj = _cnpj,
                Descricao = _descricao
            };

            return new Fornecedor(fornecedordto);
        }
    }
}
