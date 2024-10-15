namespace API.Models;

public class Atividade(Detento detento)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? DetentoId { get; set; } = detento.Id;
    public int Contador { get; set; } = 0;
    public Detento? Detento { get; set; } = detento;

}
