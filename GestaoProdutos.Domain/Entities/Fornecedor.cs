namespace GestaoProdutos.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public string Descricao { get; private set; }
        public string Cnpj { get; private set; }
    }
}