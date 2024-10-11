using System;

namespace API.Models;

public class Detento : Pessoa
{
  public int TempoPenaInicial { get; set; }
  public int PenaRestante { get; set; }
  public DateTime InicioPena { get; set; }
  public DateTime FimPena { get; set; }
  public ICollection<AtividadeDetento>? AtividadesDetento { get; set; }
  /* ana: é um teste, depois vemos o que vamos fazer */
}
