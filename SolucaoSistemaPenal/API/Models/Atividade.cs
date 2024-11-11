namespace API.Models;

public abstract class Atividade
{
    public string AtividadeId { get; set; } = Guid.NewGuid().ToString();
    public string DetentoId { get; set; } = null!;
    public int Contador { get; set; }
    public string Tipo { get; set; } = null!;
}
