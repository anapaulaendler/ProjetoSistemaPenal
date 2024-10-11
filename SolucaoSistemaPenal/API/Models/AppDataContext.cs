using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext{

    public DbSet<Atividade> TabelaAtividades { get; set; }

    public DbSet<Detento> TabelaDetentos { get; set; }

    public DbSet<Pessoa> TabelaPessoas { get; set; }
    public DbSet<AtividadeDetento> TabelaAtividadesDetento { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaPenal.db");
    }

    /* ana: abaixo é uma TENTATIVA de relação entre Detento e AtividadeDetento. 
    quando eu digo tentativa é porque 1. não tenho muita certeza do que tou fazendo
    2. só deus sabe dizer o quão longe o stackoverflow consegue me levar... 
    3. não aguentava mais os erros de migration tipo eu Precisava corrigir isso
    + !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! lembrar de conversar com o pedro e o cauê */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detento>()
            .HasMany(d => d.AtividadesDetento)
            .WithOne(a => a.Detento)
            .HasForeignKey(a => a.DetentoId);
            /* anan: acho que é autoexplicativo
            um detento pode ter várias atividades (conferir) 
            e se refere pela id de detento */
        
        
        modelBuilder.Entity<AtividadeDetento>()
            .HasOne(a => a.Leitura) 
            .WithMany();
            // TODO: precisa implementar restricao em leitura aqui? 

        modelBuilder.Entity<AtividadeDetento>()
            .HasOne(a => a.Trabalho)
            .WithMany();
        
        modelBuilder.Entity<AtividadeDetento>()
            .HasOne(a => a.Estudo)
            .WithMany();
        
        // ana: de novo, meio autoexplicativo, mas cada uma se refere ao tipo de atividade que existem
    }
};