﻿using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Filters;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task InserirProduto(ProdutoDto produtoDto)
        {
            var produto = new Produto(produtoDto);

            ValidarEntidade(produto);

            await _produtoRepository.Inserir(produto);
        }

        private void ValidarEntidade(Produto produto)
        {
            var validationResult = produto.IsValid();
            if (validationResult != null && validationResult.Errors.Any())
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task AtualizarProduto(ProdutoDto produtoDto)
        {
            var produto = await _produtoRepository.RecuperarPorId(produtoDto.Id);

            if (produto is null)
            {
                return;
            }

            produto.Atualizar(produtoDto);
            ValidarEntidade(produto);

            await _produtoRepository.Atualizar(produto);
        }

        public async Task ExcluirProduto(long id)
        {
            var produto = await _produtoRepository.RecuperarPorId(id);

            if (produto is null)
                return;

            produto.Desativar();

            await _produtoRepository.Atualizar(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> ListarTodosProdutos()
        {
            var listaProdutos = await _produtoRepository.ListarTodos();
            if (listaProdutos is null || !listaProdutos.Any())
                return null;

            return MontarListaProdutoDto(listaProdutos);
        }

        public async Task<PaginacaoDto<ProdutoDto>> ListarProdutosComFiltroEPaginacao(ProdutoFiltro filtro)
        {
            var produtosFiltrados = await _produtoRepository.ListarComFiltroEPaginacao(filtro);


            return new PaginacaoDto<ProdutoDto>()
            {
                Items = MontarListaProdutoDto(produtosFiltrados.Items),
                ItemsByPage = produtosFiltrados.ItemsByPage,
                PageIndex = produtosFiltrados.PageIndex,
                TotalItems = produtosFiltrados.TotalItems
            };
        }

        public async Task<ProdutoDto> RecuperarProdutoPorId(long id)
        {
            var produto = await _produtoRepository.RecuperarPorId(id);
            if (produto is null)
                return null;

            return MontarProdutoDto(produto);
        }

        private ProdutoDto MontarProdutoDto(Produto produto)
        {
            return new ProdutoDto()
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                DataFabricacao = produto.DataFabricacao,
                DataValidade = produto.DataValidade,
                FornecedorId = produto.FornecedorId,
                Situacao = produto.Situacao
            };
        }

        private IEnumerable<ProdutoDto> MontarListaProdutoDto(IEnumerable<Produto> produtos)
        {
            return produtos.Select(x => MontarProdutoDto(x));
        }
    }
}
