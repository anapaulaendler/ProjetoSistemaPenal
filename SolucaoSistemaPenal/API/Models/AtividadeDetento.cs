using System;

namespace API.Models;

public class AtividadeDetento
{
  public Atividade? Leitura { get; set; }
  public Atividade? Estudo { get; set; }
  public Atividade? Trabalho { get; set; }

  /* ana: apesar da falta de flexibilidade desse implementação,
  quando comparado com as outras opções, ele aparenta ser mais viável,
  principalmente porque a lei paranaense oferece somente essas três
  modalidades para a redução de pena, e por isso a equipe optou por
  fazer assim */

  
}
