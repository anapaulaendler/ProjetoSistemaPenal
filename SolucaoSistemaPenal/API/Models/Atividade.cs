using System.ComponentModel.DataAnnotations;

namespace API.Models;

public abstract class Atividade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DetentoId { get; set; }
    public required Detento Detento { get; set; }
    public int Contador { get; set; }
}
