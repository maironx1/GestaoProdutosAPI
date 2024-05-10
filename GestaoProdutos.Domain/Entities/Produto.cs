using FluentValidation.Results;
using GestaoProdutos.Domain.Validators;
using System;

namespace GestaoProdutos.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string Descricao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public long? FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; }

        public void Ativar()
        {
            Situacao = "A";
        }

        public void Desativar()
        {
            Situacao = "I";
        }

        public ValidationResult IsValid() => new ProdutoValidator().Validate(this);
    }
}
