namespace API.Models;

public class Atividade
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public int Contador { get; set; } = 0;
  
}
