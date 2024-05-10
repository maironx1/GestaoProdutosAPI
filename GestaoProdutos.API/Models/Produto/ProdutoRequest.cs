using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoProdutos.API.Models.Produto
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A data de fabricação é obrigatória.")]
        public DateTime? DataFabricacao { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A data de validade é obrigatória.")]
        public DateTime? DataValidade { get; set; }

        public long? FornecedorId { get; set; }
    }
}
