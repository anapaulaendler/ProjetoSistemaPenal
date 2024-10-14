namespace API.Models;

public class Atividade
{
  public Atividade(string detentoId)
  {
    DetentoId = detentoId;
  }
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public int Contador { get; set; } = 0;
  public string DetentoId { get; set; }
  
}
