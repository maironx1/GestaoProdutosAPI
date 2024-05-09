using FluentAssertions;
using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Services;
using GestaoProdutos.Tests.Builders;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GestaoProdutos.Tests.Services
{
    public class FornecedorServiceTests
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly FornecedorService _fornecedorService;

        public FornecedorServiceTests()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _fornecedorService = new FornecedorService(_fornecedorRepository);
        }

        [Fact]
        public async Task DeveInserirFornecedor()
        {
            //arrange
            var dto = new FornecedorDto()
            {
                Descricao = "Test",
                Cnpj = "Test"
            };

            //action
            await _fornecedorService.InserirFornecedor(dto);

            //assert
            await _fornecedorRepository.Received(1)
                .Inserir(Arg.Is<Fornecedor>(x =>
                x.Descricao == dto.Descricao
                && x.Cnpj == dto.Cnpj
                ));
        }

        [Fact]
        public async Task DeveRetoornarTodosFornecedores()
        {
            //arrange
            var fornecedor = new FornecedorBuilder().Build();
            _fornecedorRepository.ListarTodos().Returns(new List<Fornecedor>() { fornecedor });
            //action
            var result = await _fornecedorService.ListarTodosFornecedores();

            //assert
            result.Should().HaveCount(1);
            result.Should().Contain(x => x.Cnpj == fornecedor.Cnpj && x.Descricao == fornecedor.Descricao);
        }

        [Fact]
        public async Task NaoDeveRetornarFornecedoresPorNaoEncontrar()
        {
            //arrange
            _fornecedorRepository.ListarTodos().Returns(new List<Fornecedor>());

            //action
            var result = await _fornecedorService.ListarTodosFornecedores();

            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task DeveAtualizarFornecedor()
        {
            //arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "Test",
                Cnpj = "Test"
            };

            var fornecedor = new FornecedorBuilder().Build();
            _fornecedorRepository.RecuperarPorId(dto.Id).Returns(fornecedor);

            //action
            await _fornecedorService.Update(dto);

            //assert
            await _fornecedorRepository.Received(1)
                .Atualizar(Arg.Is<Fornecedor>(x =>
                x.Descricao == dto.Descricao
                && x.Cnpj == dto.Cnpj
                ));
        }

        [Fact]
        public async Task NaoDeveAtualizarFornecedorPorNaoAcharId()
        {
            //arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "Test",
                Cnpj = "Test"
            };

            //action
            await _fornecedorService.Update(dto);

            //assert
            await _fornecedorRepository.Received(1)
                .Inserir(Arg.Is<Fornecedor>(x =>
                x.Descricao == dto.Descricao
                && x.Cnpj == dto.Cnpj
                ));
        }
    }
}
