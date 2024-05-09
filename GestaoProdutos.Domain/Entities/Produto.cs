using FluentValidation.Results;
using GestaoProdutos.Domain.Dtos;
using GestaoProdutos.Domain.Validators;
using System;

namespace GestaoProdutos.Domain.Entities
{
    public class Produto : EntityBase
    {
        public Produto() { }

        public Produto(ProdutoDto dto)
        {
            Descricao = dto.Descricao;
            DataFabricacao = dto.DataFabricacao;
            DataValidade = dto.DataValidade;
            FornecedorId = dto.FornecedorId;
            Situacao = dto.Situacao;
        }

        public string Descricao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public long? FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; }

        public void Ativar()
        {
            Situacao = "A";
        }

        public void Desativar()
        {
            Situacao = "I";
        }

        public void Atualizar(ProdutoDto produtoDto)
        {
            Descricao = produtoDto.Descricao;
            DataFabricacao = produtoDto.DataFabricacao;
            DataValidade = produtoDto.DataValidade;
            FornecedorId = produtoDto.FornecedorId;
            Situacao = produtoDto.Situacao;
        }

        public ValidationResult IsValid() => new ProductValidator().Validate(this);
    }
}
