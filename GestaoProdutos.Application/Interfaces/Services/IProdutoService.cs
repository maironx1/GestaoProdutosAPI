using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Interfaces.Services
{
    public interface IProdutoService
    {
        Task InserirProduto(ProdutoDto produtoDto);
        Task AtualizarProduto(ProdutoDto produtoDto);
        Task RemoverProduto(long id);
        Task<IEnumerable<ProdutoDto>> ListarTodosProdutos();
        Task<PaginacaoDto<ProdutoDto>> ListarProdutosComFiltroEPaginacao(ProdutoFiltro filtro);
        Task<ProdutoDto> RecuperarProdutoPorId(long id);
    }
}
