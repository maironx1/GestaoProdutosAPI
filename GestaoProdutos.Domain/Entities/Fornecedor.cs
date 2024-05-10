namespace GestaoProdutos.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public string Descricao { get; set; }
        public string Cnpj { get; set; }

        public void Ativar()
        {
            Situacao = "A";
        }

        public void Desativar()
        {
            Situacao = "I";
        }
    }
}