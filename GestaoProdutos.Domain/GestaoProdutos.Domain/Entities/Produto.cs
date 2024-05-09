using System;

namespace GestaoProdutos.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? FornecedorId { get; set; }

        // Navegação para o Fornecedor
        public Fornecedor Fornecedor { get; set; }
    }
}
