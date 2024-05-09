using GestaoProdutos.Domain.Dtos;

namespace GestaoProdutos.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        protected Fornecedor() { }

        public Fornecedor(FornecedorDto dto)
        {
            Descricao = dto.Descricao;
            Cnpj = dto.Cnpj;
            Situacao = dto.Situacao;
        }
        public string Descricao { get; private set; }
        public string Cnpj { get; private set; }

        public void Atualizar(FornecedorDto fornecedorDto)
        {
            Descricao = fornecedorDto.Descricao;
            Cnpj = fornecedorDto.Cnpj;
        }
    }
}