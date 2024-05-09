using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IGenericoRepository<Produto>
    {
        Task<Paginacao<Produto>> ListarComFiltroEPaginacao(ProdutoFiltro filtro);
    }
}
