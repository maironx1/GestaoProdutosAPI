using System;

namespace GestaoProdutos.Application.Dtos
{
    public class ProdutoDto
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public long? FornecedorId { get; set; }
    }
}
