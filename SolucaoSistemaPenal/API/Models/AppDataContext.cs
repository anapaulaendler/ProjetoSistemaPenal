using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
    public DbSet<Leitura> TabelaLeitura { get; set; }
    public DbSet<Estudo> TabelaEstudo { get; set; }
    public DbSet<Trabalho> TabelaTrabalho { get; set; }
    public DbSet<Atividade> TabelaAtividades { get; set; }
    public DbSet<Detento> TabelaDetentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaPenal.db");
    }
};