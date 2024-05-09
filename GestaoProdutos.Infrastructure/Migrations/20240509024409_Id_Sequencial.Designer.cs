﻿// <auto-generated />
using System;
using GestaoProdutos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestaoProdutos.Infrastructure.Migrations
{
    [DbContext(typeof(GestaoProdutosContext))]
    [Migration("20240509024409_Id_Sequencial")]
    partial class Id_Sequencial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("GestaoProdutos.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("GestaoProdutos.Domain.Entities.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DataFabricacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataValidade")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("FornecedorId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("GestaoProdutos.Domain.Entities.Produto", b =>
                {
                    b.HasOne("GestaoProdutos.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });
#pragma warning restore 612, 618
        }
    }
}
