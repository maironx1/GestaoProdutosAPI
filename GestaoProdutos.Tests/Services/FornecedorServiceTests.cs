using FluentAssertions;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Application.Services;
using GestaoProdutos.Tests.Builders;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using GestaoProdutos.Application.Interfaces.Services;
using AutoMapper;

namespace GestaoProdutos.Tests.Services
{
    public class FornecedorServiceTests
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorServiceTests()
        {
            _fornecedorRepository = Mock.Of<IFornecedorRepository>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Fornecedor>(It.IsAny<FornecedorDto>()))
                .Returns(new Fornecedor
                {
                    Cnpj = "123456",
                    Descricao = "teste",
                    Situacao = "A",
                    Id = 1
                });

            mapperMock.Setup(m => m.Map<FornecedorDto>(It.IsAny<Fornecedor>()))
                .Returns(new FornecedorDto
                {
                    Cnpj = "123456",
                    Descricao = "teste",
                    Situacao = "A",
                    Id = 1
                });

            _mapper = mapperMock.Object;

            _fornecedorService = new FornecedorService(_fornecedorRepository, _mapper);
        }

        [Fact]
        public async Task InserirFornecedor_QuandoFornecedorValido_DeveCriarFornecedor()
        {
            // Arrange
            var dto = new FornecedorDto()
            {
                Descricao = "teste",
                Cnpj = "123456"
            };

            // Action
            await _fornecedorService.InserirFornecedor(dto);

            // Assert
            Mock.Get(_fornecedorRepository).Verify(r => r.Inserir(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Fact]
        public async Task ListarTodosFornecedors_QuandoFornecedorValido_DeveRetornarTodosFornecedors()
        {
            // Arrange
            var fornecedor = new FornecedorBuilder(_mapper).Build();
            Mock.Get(_fornecedorRepository).Setup(r => r.ListarTodos()).ReturnsAsync(new List<Fornecedor>() { fornecedor });

            // Action
            var result = await _fornecedorService.ListarTodosFornecedores();

            // Assert
            result.Should().HaveCount(1);
            result.Should().ContainEquivalentOf(fornecedor);
        }

        [Fact]
        public async Task ListarTodosFornecedors_QuandoFornecedorVazio_NaoDeveRetornarNenhumFornecedor()
        {
            // Arrange
            Mock.Get(_fornecedorRepository).Setup(r => r.ListarTodos()).ReturnsAsync(new List<Fornecedor>());

            // Action
            var result = await _fornecedorService.ListarTodosFornecedores();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task AtualizarFornecedor_QuandoFornecedorValido_DeveAtualizarFornecedor()
        {
            // Arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "teste",
                Cnpj = "123456"
            };

            var fornecedor = new FornecedorBuilder(_mapper).Build();
            Mock.Get(_fornecedorRepository).Setup(r => r.RecuperarPorId(dto.Id)).ReturnsAsync(fornecedor);

            // Action
            await _fornecedorService.AtualizarFornecedor(dto);

            // Assert
            Mock.Get(_fornecedorRepository).Verify(r => r.Atualizar(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarFornecedor_QuandoFornecedorInvalido_NaoDeveAtualizarFornecedor()
        {
            // Arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "teste",
                Cnpj = "123456"
            };

            // Action
            await _fornecedorService.AtualizarFornecedor(dto);

            // Assert
            Mock.Get(_fornecedorRepository).Verify(r => r.Inserir(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Fact]
        public async Task RemoverFornecedor_QuandoEncontraId_DeveAtualizarFornecedor()
        {
            // Arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            var fornecedor = new FornecedorBuilder(_mapper).Build();
            Mock.Get(_fornecedorRepository).Setup(r => r.RecuperarPorId(dto.Id)).ReturnsAsync(fornecedor);

            // Action
            await _fornecedorService.RemoverFornecedor(dto.Id);

            // Assert
            Mock.Get(_fornecedorRepository).Verify(r => r.Atualizar(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Fact]
        public async Task RemoverFornecedor_QuandoNaoEncontraId_NaoDeveAtualizarFornecedor()
        {
            // Arrange
            var dto = new FornecedorDto()
            {
                Id = 1,
                Descricao = "teste"
            };

            // Action
            await _fornecedorService.RemoverFornecedor(dto.Id);

            // Assert
            Mock.Get(_fornecedorRepository).Verify(r => r.Atualizar(It.IsAny<Fornecedor>()), Times.Never);
        }

        [Fact]
        public async Task RecuperarPorId_QuandoIdValido_DeveRetornarFornecedorPorId()
        {
            // Arrange
            var id = 1;
            var fornecedor = new FornecedorBuilder(_mapper).Build();
            Mock.Get(_fornecedorRepository).Setup(r => r.RecuperarPorId(id)).ReturnsAsync(fornecedor);

            // Action
            var result = await _fornecedorService.RecuperarFornecedorPorId(id);

            // Assert
            result.Should().BeEquivalentTo(fornecedor);
        }

        [Fact]
        public async Task RecuperarPorId_QuandoCnpjValido_DeveRetornarFornecedorPorCnpj()
        {
            // Arrange
            var cnpj = "123456";
            var fornecedor = new FornecedorBuilder(_mapper).Build();
            Mock.Get(_fornecedorRepository).Setup(r => r.RecuperarPorCnpj(cnpj)).ReturnsAsync(fornecedor);

            // Action
            var result = await _fornecedorService.RecuperarFornecedorPorCnpj(cnpj);

            // Assert
            result.Should().BeEquivalentTo(fornecedor);
        }
    }
}
