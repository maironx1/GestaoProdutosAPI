using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace GestaoProdutos.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<FornecedorDto> RecuperarFornecedorPorId(long id)
        {
            var fornecedor = await _fornecedorRepository.RecuperarPorId(id);
            return _mapper.Map<FornecedorDto>(fornecedor);
        }

        public async Task InserirFornecedor(FornecedorDto fornecedorDto)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
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

        public async Task AtualizarFornecedor(FornecedorDto fornecedorDto)
        {
            var fornecedor = await _fornecedorRepository.RecuperarPorId(fornecedorDto.Id);
            if (fornecedor == null)
            {
                await InserirFornecedor(fornecedorDto);
                return;
            }

            _mapper.Map(fornecedorDto, fornecedor);
            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task<FornecedorDto> RecuperarFornecedorPorCnpj(string cnpj)
        {
            var fornecedor = await _fornecedorRepository.RecuperarPorCnpj(cnpj);
            return _mapper.Map<FornecedorDto>(fornecedor);
        }
    }
}
