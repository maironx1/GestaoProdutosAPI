using System;

namespace GestaoProdutos.API.Models.Produto
{
    public class ProdutoRequest
    {
        public string Descricao { get; set; }

        public DateTime? DataFabricacao { get; set; }

        public DateTime? DataValidade { get; set; }

        public long? FornecedorId { get; set; }
    }
}
