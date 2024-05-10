using GestaoProdutos.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Interfaces.Services
{
    public interface IFornecedorService
    {
        Task InserirFornecedor(FornecedorDto fornecedorDto);
        Task<IEnumerable<FornecedorDto>> ListarTodosFornecedores();
        Task AtualizarFornecedor(FornecedorDto fornecedorDto);
        Task<FornecedorDto> RecuperarFornecedorPorId(long id);
        Task<FornecedorDto> RecuperarFornecedorPorCnpj(string cnpj);
    }
}
