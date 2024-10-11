using System;

namespace API.Models;

public class AtividadeDetento
{
      public AtividadeDetento()
  {
    Id = Guid.NewGuid().ToString();
  }
    public string Id { get; set; } // pk
    public string? DetentoId { get; set; } // fk
    public Detento? Detento { get; set; } 
    /* ana: CONVERSAR COM O PEDRO. criado pra resolver uns problemas que tava dando
    nas migrations pela relação entre Detento e AtividadeDetento não estarem claras */
  public Atividade? Leitura { get; set; }
  public Atividade? Estudo { get; set; }
  public Atividade? Trabalho { get; set; }

  /* ana: apesar da falta de flexibilidade desse implementação,
  quando comparado com as outras opções, ela aparenta ser a mais viável,
  principalmente porque a lei paranaense oferece somente essas três
  modalidades para a redução de pena, e por isso a equipe optou por
  fazer assim, já que o quesito flexibilidade nesse caso não é tão relevante */

}
