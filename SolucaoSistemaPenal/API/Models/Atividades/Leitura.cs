namespace API.Models;

public class Leitura(Detento detento) : Atividade(detento)
{
    public int Limite { get; set; }
  public int AnoAtual { get; set; } = DateTime.Now.Year;
  public double Equivalencia { get; } = 0.25;
}
