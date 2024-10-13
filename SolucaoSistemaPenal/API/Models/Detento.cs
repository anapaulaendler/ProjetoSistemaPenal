using System;

namespace API.Models;

public class Detento : Pessoa
{
  public int TempoPenaInicial { get; set; }
  public int PenaRestante { get; set; }
  public DateTime InicioPena { get; set; }
  public DateTime FimPena { get; set; }
  public Atividade Estudo { get; set; } = new Estudo();
  public Atividade Leitura { get; set; } = new Leitura();
  public Atividade Trabalho { get; set; } = new Trabalho();

}
