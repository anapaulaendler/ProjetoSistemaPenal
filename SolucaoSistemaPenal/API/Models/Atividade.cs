using System.ComponentModel.DataAnnotations;

namespace API.Models;

public abstract class Atividade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string DetentoId { get; set; } = null!;
    [Required]
    public Detento Detento { get; set; } = null!;
    public int Contador { get; set; }
}
