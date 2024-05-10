using System;

namespace GestaoProdutos.API.Models.Produto
{
    public class ProdutoResponse
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public long? FornecedorId { get; set; }

    }
}
