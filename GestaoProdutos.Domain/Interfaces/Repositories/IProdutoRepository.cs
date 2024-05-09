using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IGenericoRepository<Produto>
    {
        Task<PaginacaoDto<Produto>> ListarComFiltroEPaginacao(ProdutoFiltro filtro);
    }
}
