using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoProdutos.Infrastructure.Repositories
{
    public class ProdutoRepository : GenericoRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(GestaoProdutosContext dbContext) : base(dbContext)
        {
        }

        public async Task<Paginacao<Produto>> ListarComFiltroEPaginacao(ProdutoFiltro filtro)
        {
            var query = _dbContext.Set<Produto>()
                .Include(x => x.Fornecedor)
                .AsQueryable();

            query = AplicarFiltro(filtro, query);
            var totalItems = query.Count();

            query = AplicarPaginacao(filtro, query);
            var produtos = await query.ToListAsync();

            return new Paginacao<Produto>
            {
                Items = produtos,
                TotalItems = totalItems,
                ItemsByPage = filtro.ItemsByPage,
                PageIndex = filtro.PageIndex
            };
        }

        private static IQueryable<Produto> AplicarPaginacao(ProdutoFiltro filtro, IQueryable<Produto> query)
        {
            return query.Skip((filtro.PageIndex - 1) * filtro.ItemsByPage)
                            .Take(filtro.ItemsByPage);
        }

        private IQueryable<Produto> AplicarFiltro(ProdutoFiltro filtro, IQueryable<Produto> query)
        {
            if (filtro.Situacao != null)
                query = query.Where(x => x.Situacao == filtro.Situacao);
            if (filtro.Descricao != null)
                query = query.Where(x => x.Descricao.Contains(filtro.Descricao));
            if (filtro.DataFabricacao.HasValue)
                query = query.Where(x => x.DataFabricacao.Value.Date == filtro.DataFabricacao);
            if (filtro.DataValidade.HasValue)
                query = query.Where(x => x.DataValidade.Value.Date == filtro.DataValidade);
            if (filtro.FornecedorId != null)
                query = query.Where(x => x.Fornecedor.Id == filtro.FornecedorId);
            if (filtro.Cnpj != null)
                query = query.Where(x => x.Fornecedor.Cnpj.Contains(filtro.Cnpj));
            return query;
        }

        public override async Task<Produto> RecuperarPorId(long id)
        {
            return await _dbContext.Set<Produto>()
                .Include(x => x.Fornecedor)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
