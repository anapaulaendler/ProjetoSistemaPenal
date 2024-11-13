namespace API.Models;

public class DetentoInativo : Pessoa
{
  public string DetentoInativoId { get; set; } = Guid.NewGuid().ToString();
  public DateTime InicioPena { get; set; }
  public DateTime FimPena { get; set; }
  public List<Atividade> Atividades { get; set; } = [];
}