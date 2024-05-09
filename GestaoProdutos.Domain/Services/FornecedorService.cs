using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task InserirFornecedor(FornecedorDto fornecedorDto)
        {
            var fornecedor = new Fornecedor(fornecedorDto);
            await _fornecedorRepository.Inserir(fornecedor);
        }

        public async Task<IEnumerable<FornecedorDto>> ListarTodosFornecedores()
        {
            var fornecedores = await _fornecedorRepository.ListarTodos();
            return fornecedores.Select(x => new FornecedorDto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Cnpj = x.Cnpj,
                Situacao = x.Situacao
            });
        }

        public async Task Update(FornecedorDto fornecedorDto)
        {
            var fornecedores = await _fornecedorRepository.RecuperarPorId(fornecedorDto.Id);
            if (fornecedores == null)
            {
                await InserirFornecedor(fornecedorDto);
                return;
            }

            fornecedores.Atualizar(fornecedorDto);
            await _fornecedorRepository.Atualizar(fornecedores);
        }
    }
}
