namespace API.Models;

public abstract class Pessoa
{
    public string? Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? CPF { get; set; }
    public char Sexo { get; set; }
}
