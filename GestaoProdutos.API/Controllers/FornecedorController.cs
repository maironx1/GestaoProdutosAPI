using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestaoProdutos.API.Models.Erro;
using GestaoProdutos.API.Models.Fornecedor;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("/InserirFornecedor")]
        public async Task<IActionResult> InserirFornecedor([FromBody] FornecedorRequest fornecedorRequest)
        {
            try
            {
                if (fornecedorRequest == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o objeto FornecedorRequest não pode ser nulo."
                    };

                    return BadRequest(errorResponse);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var fornecedorDto = _mapper.Map<FornecedorDto>(fornecedorRequest);

                await _fornecedorService.InserirFornecedor(fornecedorDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpPut("/AtualizarFornecedor/{fornecedorId}")]
        public async Task<IActionResult> AtualizarFornecedor(long fornecedorId, [FromBody] FornecedorRequest fornecedorRequest)
        {
            try
            {
                if (fornecedorId == 0)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o ID do fornecedor não pode ser zero."
                    };

                    return BadRequest(errorResponse);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var fornecedorDto = _mapper.Map<FornecedorDto>(fornecedorRequest);
                fornecedorDto.Id = fornecedorId;

                await _fornecedorService.AtualizarFornecedor(fornecedorDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpDelete("/RemoverFornecedor/{fornecedorId}")]
        public async Task<IActionResult> RemoverFornecedor([FromRoute] long fornecedorId)
        {
            try
            {
                if (fornecedorId == 0)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o ID do fornecedor não pode ser zero"
                    };

                    return BadRequest(errorResponse);
                }

                await _fornecedorService.RemoverFornecedor(fornecedorId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpGet("/ListarFornecedores")]
        public async Task<IActionResult> ListarFornecedores()
        {
            var fornecedores = await _fornecedorService.ListarTodosFornecedores();
            var fornecedoresResponse = fornecedores.Select(f => _mapper.Map<FornecedorResponse>(f));

            return Ok(fornecedoresResponse);
        }

        [HttpGet("/RecuperarFornecedorPorId/{fornecedorId}")]
        public async Task<IActionResult> RecuperarFornecedorPorId(long fornecedorId)
        {
            try
            {
                var fornecedorDto = await _fornecedorService.RecuperarFornecedorPorId(fornecedorId);

                if (fornecedorDto == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o ID do fornecedor não foi encontrado."
                    };

                    return NotFound(errorResponse);
                }

                var fornecedorResponse = _mapper.Map<FornecedorResponse>(fornecedorDto);
                return Ok(fornecedorResponse);
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpGet("/RecuperarFornecedorPorCnpj/{cnpj}")]
        public async Task<IActionResult> RecuperarFornecedorPorCnpj(string cnpj)
        {
            try
            {
                var fornecedorDto = await _fornecedorService.RecuperarFornecedorPorCnpj(cnpj);

                if (fornecedorDto == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o CNPJ do fornecedor não foi encontrado."
                    };

                    return NotFound(errorResponse);
                }

                var fornecedorResponse = _mapper.Map<FornecedorResponse>(fornecedorDto);
                return Ok(fornecedorResponse);
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        private ObjectResult BuildError(string mensagem)
        {
            return BadRequest(new ErrorResponse
            {
                Codigo = "Dados inválidos",
                Mensagem = mensagem
            });
        }
    }
}
