namespace API.Models;

public abstract class Atividade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DetentoId { get; set; } = null!;
    public int Contador { get; set; }

    public Atividade() {}
    
}
