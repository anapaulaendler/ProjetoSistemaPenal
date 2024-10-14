namespace API.Models;

public class Estudo : Atividade
{
    public Estudo(string detentoId) : base(detentoId)
    {
    }

    public int Equivalencia { get; } = 3;
}
