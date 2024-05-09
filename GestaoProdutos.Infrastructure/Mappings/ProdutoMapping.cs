using GestaoProdutos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoProdutos.Infrastructure.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Descricao).IsRequired();
            builder.Property(e => e.Situacao).IsRequired();
            builder.Property(e => e.DataFabricacao);
            builder.Property(e => e.DataValidade);
            builder.HasOne(e => e.Fornecedor)
                  .WithMany()
                  .HasForeignKey(e => e.FornecedorId)
                  .IsRequired();
        }
    }
}
