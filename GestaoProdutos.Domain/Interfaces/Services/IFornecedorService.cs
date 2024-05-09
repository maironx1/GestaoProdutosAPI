using GestaoProdutos.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Services
{
    public interface IFornecedorService
    {
        public Task InserirFornecedor(FornecedorDto fornecedorDto);
        public Task<IEnumerable<FornecedorDto>> ListarTodosFornecedores();
        public Task Update(FornecedorDto fornecedorDto);
    }
}
