using System;

namespace API.Models;

public class Pessoa
{

    public Pessoa()
    {
    Id = Guid.NewGuid().ToString();
    }
  public string Id { get; set; }
  public string? Nome { get; set; }
  public string? DataNascimento { get; set; }
  public string? CPF { get; set; }
  public char Sexo { get; set; }

}
