using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext{

    public DbSet<Atividade> TabelaAtividades {get ; set;}

    public DbSet<Detento> TabelaDetentos {get ; set;}

    public DbSet<Pessoa> TabelaPessoas {get ; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaPenal.db");
    }
};