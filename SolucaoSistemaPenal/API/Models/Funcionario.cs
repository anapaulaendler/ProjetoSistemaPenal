namespace API.Models;

public class Funcionario : Pessoa
{
    public string FuncionarioId { get; set; } = Guid.NewGuid().ToString();
    public required string Cargo { get; set; } 
    public string? Senha { get; set; }
    // o cargo precisa aparecer pra gente definir se o funcionário pode
    // alterar a pena

    // "Diretor(a) do Presídio" / "Agente Penitenciário Especializado" / "Assistente Social": PODE
    // "Agente de Segurança Penitenciária (ASP)" / "Chefe de Vigilância e Disciplina" / "Inspetor Penitenciário" / 
    // "Enfermeiro(a) Penitenciário(a)" / "Médico(a) Penitenciário(a)" / "Coordenador(a) de Educação e Trabalho" / 
    // "Professor(a) de Educação Prisional" / "Nutricionista" / "Cozinheiro(a)" / "Assistente Administrativo": NAO PODE
}