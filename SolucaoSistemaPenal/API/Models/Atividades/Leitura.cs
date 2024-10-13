namespace API.Models;

public class Leitura : Atividade
{

  public Leitura(){
    AnoAtual = DateTime.Now.Year;
    Contador = 0;
  }
  public int Limite { get; set; }
  public int AnoAtual { get; set; }
  public int Contador { get; set; }
  public double Equivalencia { get; } = 0.25;
}
