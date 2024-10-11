﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20241011234931_MudandoTabelaDetento")]
    partial class MudandoTabelaDetento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("API.Models.Atividade", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AnoAtual")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Contador")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Equivalencia")
                        .HasColumnType("REAL");

                    b.Property<string>("Limite")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TabelaAtividades");
                });

            modelBuilder.Entity("API.Models.AtividadeDetento", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("DetentoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EstudoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LeituraId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TrabalhoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DetentoId");

                    b.HasIndex("EstudoId");

                    b.HasIndex("LeituraId");

                    b.HasIndex("TrabalhoId");

                    b.ToTable("TabelaAtividadesDetento");
                });

            modelBuilder.Entity("API.Models.Pessoa", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sexo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TabelaPessoas");

                    b.HasDiscriminator().HasValue("Pessoa");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Detento", b =>
                {
                    b.HasBaseType("API.Models.Pessoa");

                    b.Property<DateTime>("FimPena")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InicioPena")
                        .HasColumnType("TEXT");

                    b.Property<int>("PenaRestante")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TempoPenaInicial")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Detento");
                });

            modelBuilder.Entity("API.Models.AtividadeDetento", b =>
                {
                    b.HasOne("API.Models.Detento", "Detento")
                        .WithMany("AtividadesDetento")
                        .HasForeignKey("DetentoId");

                    b.HasOne("API.Models.Atividade", "Estudo")
                        .WithMany()
                        .HasForeignKey("EstudoId");

                    b.HasOne("API.Models.Atividade", "Leitura")
                        .WithMany()
                        .HasForeignKey("LeituraId");

                    b.HasOne("API.Models.Atividade", "Trabalho")
                        .WithMany()
                        .HasForeignKey("TrabalhoId");

                    b.Navigation("Detento");

                    b.Navigation("Estudo");

                    b.Navigation("Leitura");

                    b.Navigation("Trabalho");
                });

            modelBuilder.Entity("API.Models.Detento", b =>
                {
                    b.Navigation("AtividadesDetento");
                });
#pragma warning restore 612, 618
        }
    }
}
