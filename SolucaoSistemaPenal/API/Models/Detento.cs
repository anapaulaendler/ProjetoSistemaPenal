using System;

namespace API.Models;

public class Detento : Pessoa
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public int TempoPenaInicial { get; set; }
  public int PenaRestante { get; set; }
  public string? InicioPena { get; set; }
  public string? FimPena { get; set; }
  public ICollection<Atividade> Atividades { get; set; } = new List<Atividade>();
}
