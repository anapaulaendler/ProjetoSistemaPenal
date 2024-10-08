using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext{

    public DbSet<Atividade> TabelaAtividade {get ; set;}

    public DbSet<Detento> TabelaDetento {get ; set;}

    public DbSet<Pessoa> TabelaPessoa {get ; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaPenal.db");
    }
};