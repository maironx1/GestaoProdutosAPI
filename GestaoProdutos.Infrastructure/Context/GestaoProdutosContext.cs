using GestaoProdutos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GestaoProdutos.Infrastructure.Context
{
    public class GestaoProdutosContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GestaoProdutosContext(DbContextOptions<GestaoProdutosContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

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
                      .IsRequired();
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