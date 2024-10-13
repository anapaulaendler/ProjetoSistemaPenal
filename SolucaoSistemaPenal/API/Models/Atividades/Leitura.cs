namespace API.Models;

public class Leitura : Atividade
{

  public Leitura(){
    AnoAtual = DateTime.Now.Year;
    
  }
  public int Limite { get; set; }
  public int AnoAtual { get; set; }
  public double Equivalencia { get; } = 0.25;
}
