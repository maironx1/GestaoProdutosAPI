using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces
{
    public interface IProdutoRepository : IGenericoRepository<Produto>
    {
        Task<IEnumerable<Produto>> ListarComFiltroEPaginacao(ProdutoFiltro filtro, int pagina, int quantidade);
    }
}
