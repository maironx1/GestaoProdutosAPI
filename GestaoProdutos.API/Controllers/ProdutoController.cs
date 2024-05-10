using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestaoProdutos.API.Models.Erro;
using GestaoProdutos.API.Models.Produto;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Application.Interfaces.Services;
using GestaoProdutos.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> InserirProduto([FromBody] ProdutoRequest produtoRequest)
        {
            try
            {
                if (produtoRequest == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o objeto ProdutoRequest não pode ser nulo."
                    };

                    return BadRequest(errorResponse);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productDto = _mapper.Map<ProdutoDto>(produtoRequest);
                await _produtoService.InserirProduto(productDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpPut("{produtoId}")]
        public async Task<IActionResult> AtualizarProduto(long produtoId, [FromBody] ProdutoRequest produtoRequest)
        {
            try
            {
                if (produtoId == 0)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o objeto ProdutoRequest não pode ser nulo."
                    };

                    return BadRequest(errorResponse);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (produtoRequest != null)
                {
                    var productDto = _mapper.Map<ProdutoDto>(produtoRequest);
                    productDto.Id = produtoId;
                    await _produtoService.AtualizarProduto(productDto);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpDelete("{produtoId}")]
        public async Task<IActionResult> RemoverProduto([FromRoute] long produtoId)
        {
            try
            {
                if (produtoId == 0)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Codigo = "Dados inválidos",
                        Mensagem = "Requisição inválida: o objeto ProdutoRequest não pode ser nulo."
                    };

                    return BadRequest(errorResponse);
                }

                await _produtoService.RemoverProduto(produtoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BuildError(ex.Message);
            }
        }

        [HttpGet]
        public async Task<PaginacaoDto<ProdutoResponse>> ListarProdutosComFiltroEPaginacao([FromQuery] ProdutoFiltro produtoFilter)
        {
            var products = await _produtoService.ListarProdutosComFiltroEPaginacao(produtoFilter);
            return new PaginacaoDto<ProdutoResponse>()
            {
                Items = products.Items.Select(x => _mapper.Map<ProdutoResponse>(x)),
                ItemsByPage = products.ItemsByPage,
                PageIndex = products.PageIndex,
                TotalItems = products.TotalItems,
            };
        }

        [HttpGet("{productId}")]
        public async Task<ProdutoResponse> RecuperarProdutoPorId([FromRoute] long produtoId)
        {
            var product = await _produtoService.RecuperarProdutoPorId(produtoId);

            if (product is null)
            {
                return default;
            }

            return _mapper.Map<ProdutoResponse>(product);
        }

        private ObjectResult BuildError(string mensagem)
        {
            return BadRequest(new ErrorResponse()
            {
                Codigo = "Dados invalidos",
                Mensagem = mensagem
            });
        }
    }
}
