namespace API.Models;

public class Trabalho : Atividade
{
  public Trabalho(string detentoId) : base(detentoId)
  {
  }
  public int Equivalencia { get; } = 3;
}
