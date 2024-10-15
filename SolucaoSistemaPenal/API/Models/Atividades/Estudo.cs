namespace API.Models;

public class Estudo(Detento detento) : Atividade(detento)
{
    public int Equivalencia { get; } = 3;
}
