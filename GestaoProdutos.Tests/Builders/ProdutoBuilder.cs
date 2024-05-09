using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Entities;
using System;

namespace GestaoProdutos.Tests.Builders
{
    public class ProdutoBuilder
    {
        private string _descricao = "Product test";
        private DateTime _dataFabricacao = DateTime.Now;
        private DateTime _dataValidade = DateTime.Now.AddDays(1);
        private long _fornecedorId = 1;
        private string _situacao = "A";

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

            return new Produto(dto);
        }
    }
}
