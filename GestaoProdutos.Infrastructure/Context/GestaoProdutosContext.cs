using GestaoProdutos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoProdutos.Infrastructure.Context
{
    public class GestaoProdutosContext : DbContext
    {
        public GestaoProdutosContext(DbContextOptions<GestaoProdutosContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.Situacao).IsRequired();
                entity.Property(e => e.DataFabricacao);
                entity.Property(e => e.DataValidade);
                entity.HasOne(e => e.Fornecedor)
                      .WithMany()
                      .HasForeignKey(e => e.FornecedorId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.CNPJ).IsRequired();
                entity.Property(e => e.Situacao).IsRequired();
            });
        }
    }
}
