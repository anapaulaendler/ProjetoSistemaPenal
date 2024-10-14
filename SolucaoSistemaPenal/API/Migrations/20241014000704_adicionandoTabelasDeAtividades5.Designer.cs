﻿// <auto-generated />
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20241014000704_adicionandoTabelasDeAtividades5")]
    partial class adicionandoTabelasDeAtividades5
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

                    b.Property<int>("Contador")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TabelaAtividades");

                    b.HasDiscriminator().HasValue("Atividade");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Models.Detento", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("EstudoId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FimPena")
                        .HasColumnType("TEXT");

                    b.Property<string>("InicioPena")
                        .HasColumnType("TEXT");

                    b.Property<string>("LeituraId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("PenaRestante")
                        .HasColumnType("INTEGER");

                    b.Property<char>("Sexo")
                        .HasColumnType("TEXT");

                    b.Property<int>("TempoPenaInicial")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrabalhoId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EstudoId");

                    b.HasIndex("LeituraId");

                    b.HasIndex("TrabalhoId");

                    b.ToTable("TabelaDetentos");
                });

            modelBuilder.Entity("API.Models.Estudo", b =>
                {
                    b.HasBaseType("API.Models.Atividade");

                    b.HasDiscriminator().HasValue("Estudo");
                });

            modelBuilder.Entity("API.Models.Leitura", b =>
                {
                    b.HasBaseType("API.Models.Atividade");

                    b.Property<int>("AnoAtual")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Limite")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Leitura");
                });

            modelBuilder.Entity("API.Models.Trabalho", b =>
                {
                    b.HasBaseType("API.Models.Atividade");

                    b.HasDiscriminator().HasValue("Trabalho");
                });

            modelBuilder.Entity("API.Models.Detento", b =>
                {
                    b.HasOne("API.Models.Atividade", "Estudo")
                        .WithMany()
                        .HasForeignKey("EstudoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Atividade", "Leitura")
                        .WithMany()
                        .HasForeignKey("LeituraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Atividade", "Trabalho")
                        .WithMany()
                        .HasForeignKey("TrabalhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudo");

                    b.Navigation("Leitura");

                    b.Navigation("Trabalho");
                });
#pragma warning restore 612, 618
        }
    }
}
