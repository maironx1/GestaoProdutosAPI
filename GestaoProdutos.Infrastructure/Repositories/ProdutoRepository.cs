using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces;
using GestaoProdutos.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Infrastructure.Repositories
{
    public class ProdutoRepository : GenericoRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(GestaoProdutosContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Produto>> ListarComFiltroEPaginacao(ProdutoFiltro filtro, int pagina, int quantidade)
        {
            throw new System.NotImplementedException();
        }
    }
}
