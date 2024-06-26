﻿using AutoMapper;
using GestaoProdutos.Application.Dtos;
using GestaoProdutos.Domain.Entities;
using System;

namespace GestaoProdutos.Tests.Builders
{
    public class ProdutoBuilder
    {
        private string _descricao = "Produto de Teste";
        private DateTime _dataFabricacao = DateTime.Now.Date;
        private DateTime _dataValidade = DateTime.Now.Date;
        private long _fornecedorId = 1;
        private string _situacao = "A";

        private readonly IMapper _mapper;

        public ProdutoBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProdutoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ProdutoBuilder ComDataFabricacao(DateTime dataFabricacao)
        {
            _dataFabricacao = dataFabricacao;
            return this;
        }

        public ProdutoBuilder ComDataValidade(DateTime dataValidade)
        {
            _dataValidade = dataValidade;
            return this;
        }

        public ProdutoBuilder ComSituacao(string situacao)
        {
            _situacao = situacao;
            return this;
        }

        public ProdutoBuilder ComFornecedorId(long fornecedorId)
        {
            _fornecedorId = fornecedorId;
            return this;
        }

        public Produto Build()
        {
            var dto = new ProdutoDto
            {
                DataFabricacao = _dataFabricacao,
                DataValidade = _dataValidade,
                Descricao = _descricao,
                Situacao = _situacao,
                FornecedorId = _fornecedorId
            };

            return _mapper.Map<Produto>(dto);
        }
    }
}
