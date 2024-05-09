using GestaoProdutos.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Interfaces.Services
{
    public interface IFornecedorService
    {
        public Task InserirFornecedor(FornecedorDto fornecedorDto);
        public Task<IEnumerable<FornecedorDto>> ListarTodosFornecedores();
        public Task Update(FornecedorDto fornecedorDto);
    }
}
