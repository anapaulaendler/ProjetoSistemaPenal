namespace API.Models;

public class Trabalho(Detento detento) : Atividade(detento)
{
    public int Equivalencia { get; } = 3;
}
