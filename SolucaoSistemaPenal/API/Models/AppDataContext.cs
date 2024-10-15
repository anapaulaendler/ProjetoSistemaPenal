using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext{
    public DbSet<Leitura> TabelaLeitura { get; set; }
    public DbSet<Estudo> TabelaEstudo { get; set; }
    public DbSet<Trabalho> TabelaTrabalho { get; set; }
    public DbSet<Atividade> TabelaAtividades { get; set; }
    public DbSet<Detento> TabelaDetentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaPenal.db");
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Detento>()
        .HasMany(e => e.Atividades)
        .WithOne(e => e.Detento)
        .HasForeignKey(e => e.DetentoId)
        .IsRequired();
}
};