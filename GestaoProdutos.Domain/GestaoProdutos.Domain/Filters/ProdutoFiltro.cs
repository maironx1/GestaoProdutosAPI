using System;

namespace GestaoProdutos.Domain.Filters
{
    public class ProdutoFiltro
    {
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? FornecedorId { get; set; }
    }
}
