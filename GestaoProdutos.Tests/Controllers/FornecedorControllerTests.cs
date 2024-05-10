using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using GestaoProdutos.API.Controllers;
using GestaoProdutos.API.Models.Erro;
using GestaoProdutos.API.Models.Fornecedor;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace GestaoProdutos.Tests.Controllers
{
    public class FornecedorControllerTests
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
        private readonly FornecedorController _fornecedorController;

        public FornecedorControllerTests()
        {
            _fornecedorService = Substitute.For<IFornecedorService>();
            _mapper = Substitute.For<IMapper>();
            _fornecedorController = new FornecedorController(_fornecedorService, _mapper);
        }

        [Fact]
        public async Task InserirFornecedor_QuandoRequestValido_DeveRetornarOk()
        {
            // Arrange
            var fornecedorRequest = new FornecedorRequest
            {
                Descricao = "Fornecedor Teste",
                Cnpj = "12345678901234"
            };

            var fornecedorDto = new FornecedorDto
            {
                Descricao = fornecedorRequest.Descricao,
                Cnpj = fornecedorRequest.Cnpj
            };

            _mapper.Map<FornecedorDto>(fornecedorRequest).Returns(fornecedorDto);

            // Act
            var result = await _fornecedorController.InserirFornecedor(fornecedorRequest);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task InserirFornecedor_QuandoRequestNulo_DeveRetornarBadRequest()
        {
            // Act
            var result = await _fornecedorController.InserirFornecedor(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var errorResponse = badRequestResult.Value as ErrorResponse;
            errorResponse.Should().NotBeNull();
            errorResponse.Codigo.Should().Be("Dados inválidos");
        }

        [Fact]
        public async Task InserirFornecedor_QuandoModelStateInvalido_DeveRetornarBadRequest()
        {
            // Arrange
            _fornecedorController.ModelState.AddModelError("Descricao", "A descrição é obrigatória.");

            // Act
            var result = await _fornecedorController.InserirFornecedor(new FornecedorRequest());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task RemoverFornecedor_IdValido_ReturnsOkResult()
        {
            // Arrange
            var produtoId = 1;

            // Act
            var result = await _fornecedorController.RemoverFornecedor(produtoId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task ListarFornecedores_QuandoHaFornecedores_DeveRetornarOkComListaDeFornecedores()
        {
            // Arrange
            var fornecedoresDto = new List<FornecedorDto>
            {
                new FornecedorDto { Id = 1, Descricao = "Fornecedor 1", Cnpj = "12345678901234" },
                new FornecedorDto { Id = 2, Descricao = "Fornecedor 2", Cnpj = "56789012345678" }
            };

            _fornecedorService.ListarTodosFornecedores().Returns(fornecedoresDto);

            // Act
            var result = await _fornecedorController.ListarFornecedores();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var fornecedoresResponse = okResult.Value as IEnumerable<FornecedorResponse>;
            fornecedoresResponse.Should().NotBeNull();
            fornecedoresResponse.Should().HaveCount(2);
        }

        [Fact]
        public async Task ListarFornecedores_QuandoNaoHaFornecedores_DeveRetornarOkComListaVazia()
        {
            // Arrange
            _fornecedorService.ListarTodosFornecedores().Returns(new List<FornecedorDto>());

            // Act
            var result = await _fornecedorController.ListarFornecedores();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var fornecedoresResponse = okResult.Value as IEnumerable<FornecedorResponse>;
            fornecedoresResponse.Should().NotBeNull();
            fornecedoresResponse.Should().BeEmpty();
        }
    }
}
