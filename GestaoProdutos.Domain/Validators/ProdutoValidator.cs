using FluentValidation;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Produto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.DataFabricacao)
                .LessThan(product => product.DataValidade)
                .WithMessage("A data de fabricação deve ser anterior à data de validade.");

            RuleFor(x => x.Descricao)
                .NotNull()
                .WithMessage("A descrição do produto deve ser informado.");

            RuleFor(x => x.FornecedorId)
                .NotEqual(0)
                .WithMessage("Deve ser informado um fornecedor valido!");
        }
    }
}
