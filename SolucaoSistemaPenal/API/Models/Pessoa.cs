using System;

namespace API.Models;

public class Pessoa
{
  public Pessoa()
  {
    Id = Guid.NewGuid().ToString();
  }
  string? Id { get; set; }
  public string? Nome { get; set; }
  public DateTime DataNascimento { get; set; }
  public int CPF { get; set; }
  public string? Sexo { get; set; }

}
