using System;

namespace API.Models;

public class Atividade
{
  public Atividade()
  {
    Id = Guid.NewGuid().ToString();
    AnoAtual = DateTime.Now.Year;
  }
  public string? Id { get; set; }
  public string? Nome { get; set; }
  public int Contador { get; set; }
  public float Equivalencia { get; set; }
  public int AnoAtual { get; set; }
  public string? Limite { get; set; }
}
