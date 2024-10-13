namespace API.Models;

public class Atividade
{
  public Atividade()
  {
    Id = Guid.NewGuid().ToString();
  }
  public string Id { get; set; }

}
