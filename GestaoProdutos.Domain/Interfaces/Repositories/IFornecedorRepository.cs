using GestaoProdutos.Domain.Entities;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository : IGenericoRepository<Fornecedor>
    {
        Task<Fornecedor> RecuperarPorCnpj(string cnpj);
    }
}
