using System;

namespace API.Models;

public class Detento : Pessoa
{
  public Detento()
  {
    Id = Guid.NewGuid().ToString();
    Estudo = new Estudo(Id);
    Leitura = new Leitura(Id);
    Trabalho = new Trabalho(Id);
  }
  public int TempoPenaInicial { get; set; }
  public int PenaRestante { get; set; }
  public string? InicioPena { get; set; }
  public string? FimPena { get; set; }
  public Atividade Estudo { get; set; }
  public Atividade Leitura { get; set; }
  public Atividade Trabalho { get; set; }

}
