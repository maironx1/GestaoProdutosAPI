namespace GestaoProdutos.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public string Descricao { get; set; }
        public string CNPJ { get; set; }
        public string Situacao { get; set; }
    }
}