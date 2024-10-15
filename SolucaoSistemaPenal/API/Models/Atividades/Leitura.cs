namespace API.Models;

public class Leitura : Atividade
{
    public int Limite { get; set; }
    public int AnoAtual { get; set; } = DateTime.Now.Year;
    public double Equivalencia { get; } = 0.25;
}
