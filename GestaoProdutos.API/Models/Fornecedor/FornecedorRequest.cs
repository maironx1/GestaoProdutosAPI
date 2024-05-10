using System.ComponentModel.DataAnnotations;

namespace GestaoProdutos.API.Models.Fornecedor
{
    public class FornecedorRequest
    {
        [Required(ErrorMessage = "A descrição do fornecedor é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Cnpj do fornecedor é obrigatória.")]
        public string Cnpj { get; set; }
    }
}
