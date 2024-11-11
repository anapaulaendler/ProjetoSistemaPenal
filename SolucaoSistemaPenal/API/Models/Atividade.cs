namespace API.Models;

public abstract class Atividade
{
    public string AtividadeId { get; set; } = Guid.NewGuid().ToString();
    public string DetentoId { get; set; } = null!;
    public int Contador { get; set; }
    public string Tipo { get; set; } = null!;
    public double Equivalencia { get; set; }
    public int Limite { get; set; }
    public int AnoAtual { get; set; }
}
