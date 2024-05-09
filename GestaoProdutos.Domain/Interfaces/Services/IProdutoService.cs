using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Task InserirProduto(ProdutoDto produtoDto);
        Task AtualizarProduto(ProdutoDto produtoDto);
        Task ExcluirProduto(long id);
        Task<IEnumerable<ProdutoDto>> ListarTodosProdutos();
        Task<PaginacaoDto<ProdutoDto>> ListarProdutosComFiltroEPaginacao(ProdutoFiltro filtro);
        Task<ProdutoDto> RecuperarProdutoPorId(long id);
    }
}
