using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoProdutos.Infrastructure.Repositories
{
    public class FornecedorRepository : GenericoRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(GestaoProdutosContext dbContext) : base(dbContext)
        {
        }

        public async Task<Fornecedor> RecuperarPorCnpj(string cnpj)
        {
            return await _dbContext.Set<Fornecedor>()
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
