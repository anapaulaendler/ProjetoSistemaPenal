namespace API.Models;

public class Detento : Pessoa
{
  public string DetentoId { get; set; } = Guid.NewGuid().ToString();
  // public int TempoPenaInicial { get; set; }
  // public int PenaRestante { get; set; }
  public DateTime InicioPena { get; set; }
  public DateTime FimPena { get; set; }
  public List<Atividade> Atividades { get; set; } = [];
}
