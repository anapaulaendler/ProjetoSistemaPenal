using System;

namespace API.Models;

public class Atividade
{
  public Atividade()
  {
    Id = Guid.NewGuid().ToString();
    AnoAtual = DateTime.Now.Year;
  }
  public string Id { get; set; }
  public string? Nome { get; set; }
  public int Contador { get; set; }
  public float Equivalencia { get; set; }
  // ana: float ou int?
  public int AnoAtual { get; set; }
  public int Limite { get; set; }
}
